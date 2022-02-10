using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogic : MonoBehaviour {

	//몬스터 이동속도
	public float movespeed = 1f;
	//몬스터 애니메이션
	Animator animator;
	//
	Vector3 movement;
	//행동= 0:Idle, 1:Leftmove, 2:Rightmove, 3:Attack
	int movementFlag=0;

	bool isTracing;

	public bool dirRight = true;	

	public Transform PlayerPos;

	void Start()
	{
		//애니메이션 레퍼런스연결
		animator=GetComponent<Animator>();

		//코루틴을 이용해서 랜덤으로 행동 진행되게 함
		StartCoroutine("ChangeMovement");
	}

	void FixedUpdate()
	{
		Move();
	}

	void Move(){

		//0.Idle 상태에서는 속도 0??
		Vector3 moveVelocity=Vector3.zero;
		//어디로 움직이는지
		string direction="";

		//Trace or Random
		if(isTracing)
		{
			//Vector3 playerPos=traceTarget.transform.position;
			if (PlayerPos.position.x < transform.position.x)
				direction = "Left";
			else if(PlayerPos.position.x>transform.position.x)
				direction="Right";
		}
		else{
			if (movementFlag == 1) {
				direction = "Left";
				dirRight = false;
			} else if (movementFlag == 2) {
				direction = "Right";
				dirRight = true;
			}
		}

		//movement Assign
		if(direction=="Left")
		{
			Debug.Log("왼쪽");
			moveVelocity=Vector3.left;
			transform.localScale=new Vector3(3,3,1);
		}
		else if(direction=="Right")
		{
			Debug.Log("오른쪽");
			moveVelocity=Vector3.right;
			transform.localScale=new Vector3(-3,3,1);
		}

		//else if(movenmentFlag==3)
		//{
		// Instantiate무기
		//}
		transform.position+=moveVelocity*movespeed*Time.deltaTime;
	}

	IEnumerator ChangeMovement()
	{
		movementFlag=Random.Range(0,4);

		if(movementFlag==0&&movementFlag==4)
			//스피드 0줘서 움직이지않게
			animator.SetFloat("Speed", 0f);

		else
			//스피드줘서 움직이게
			animator.SetFloat("Speed", 0.2f);

		//3초후 코루틴 다시시작.
		yield return new WaitForSeconds(3f);

		StartCoroutine("ChangeMovement");
	}
	//////////////////////////////////////////////////////////////
	//Trigger Maintain
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag=="Player"){
			isTracing=true;
			animator.SetBool("trace",true);
		}
	}


	//Trigger Maintain
	void OnTriggerStay2D (Collider2D col)
	{
		if(col.gameObject.tag=="Player"){
			isTracing=true;
			animator.SetBool("trace",true);
		}
	}

	//Trigger Over
	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.tag=="Player")
		{
			isTracing=false;
			StartCoroutine("ChangeMovement");
		}
	}



}
