using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
	[HideInInspector]
	public bool dirRight = false;   //어디방향보는지

	public enum MODE_KIND { ENEMY1 = 1, ENEMY2, ENEMY3 };//몬스터마다 속도나 다른 설정다르게 할 수 있음
	[Header("SETTING")]
	public MODE_KIND enemyKind = MODE_KIND.ENEMY1;

	public float moveSpeed=-0.5f;//몬스터의 이동속도 '-'면 왼쪽으로 이동
	public int HP=1;//몬스터의 생명력
	public int justtwo = 0;

	private Animator anim;//animator 컴포넌트를 위한 레퍼런스
	private SpriteRenderer ren;//SpriteRenderer 컴포넌트를 위한 레퍼런스
	private Transform frontCheck;//만약 무엇이든 몬스터 앞에 있다면 체크를 위해 사용되는 gameobject의 position을 위한 Reference
	public bool dead = false;//몬스터가 죽었는지 아닌지를 위한 변수
	public bool ATTACK;
	public bool one;
	private bool front;

	private bool falling = false;
	private bool falling2 = false;
	private bool falling3 = false;
	private Transform groundCheck;	

	public Rigidbody2D rbody2D;//무기프리팹
	public Transform deadposition;
	[HideInInspector]
	public float lastpositionX;
	[HideInInspector]
	public float lastpositionY;
	[HideInInspector]
	public float ydiff=0;
	[HideInInspector]
	public float _ydiff=0;

	Drop DoDrop;
	//float de=8f;
	int rannum=0;

	enum RandomAct{Attack=1,Move}
	int cho;

	void Start () {
		StartCoroutine (Mon1AI ());
	}

	IEnumerator Mon1AI()
	{
		cho = Random.Range (1, 2);

		rannum=Random.Range (1, 4);
		//attack
		if (cho == 1) {
			yield return new WaitForSeconds (rannum);
			anim.SetTrigger ("monattack");
			yield return new WaitForSeconds (1f);
			ATTACK = true;
			transform.Find ("monThrow").GetComponent<MonsterWeapons>().Startth ();
			moveSpeed = 0f;
			yield return new WaitForSeconds (0.5f);
			ATTACK = false;
			yield return new WaitForSeconds (1.5f);
			moveSpeed = -0.5f;
			Start ();
		
		}
		//move
		else {
			yield return new WaitForSeconds (rannum);
			Flip ();
			yield return new WaitForSeconds (rannum);
			Start ();
		}
	}
	void Awake()
	{
		//레퍼런스들의 셋팅
		groundCheck = transform.Find("groundCheck");
		anim=GetComponent<Animator>();
		frontCheck = transform.Find ("frontCheck").transform;
		rbody2D = GetComponent<Rigidbody2D> ();
		deadposition = GetComponent<Transform> ();
		DoDrop = GetComponentInChildren<Drop> ();
	
	}

	public void Hurt()
	{
		//몬스터 생명력을 1만큼 줄인다
		HP--;
	}

	// Update is called once per frame
	void Update () {

		falling=Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Ground"));
		falling2=Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Obstacle"));
		falling3=Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("mon1turn"));

		front=Physics2D.Linecast(frontCheck.position, transform.position,1 << LayerMask.NameToLayer("Wall"));
		if(front)
		{
			Flip ();
			front=false;
		}

		if (!falling || !falling2||!falling3)
		{
			//높이차가 다를때
			if(ydiff!=_ydiff)
			{
				//떨어질때 다른 애니메이션
				StartCoroutine (heightcheck ());
				anim.SetTrigger ("fall");
				falling = true;
				falling2 = true;
				falling3 = true;
			}
		}	


		if (!dead) {
			lastpositionX = deadposition.position.x;
			lastpositionY = deadposition.position.y;
		}
	}

	IEnumerator heightcheck()
	{
		ydiff=deadposition.position.y;
		yield return new WaitForSeconds (1f);
		_ydiff=deadposition.position.y;
		Debug.Log (ydiff);
		Debug.Log (_ydiff);

	}

	void FixedUpdate()
	{
		if(!dead)
		{
			//몬스터 move
			rbody2D.velocity = new Vector2 (transform.localScale.x * moveSpeed, rbody2D.velocity.y);//대문자시작:컴포넌트. 소문자시작:참조변수
		}
	

		//만약 몬스터의 HP가 0또는 0미만이고 아직 살아있다면 죽이자..
		if (HP <= 0 && !dead)
			//Death()함수 호출
			Death ();
	}


	void OnCollisionEnter2D (Collision2D col)
	{

		if (col.gameObject.tag == "Wall"||col.gameObject.tag=="Spike") {
			Flip ();
		}

	}

	void OnTriggerEnter2D(Collider2D cl)
	{
		switch (enemyKind) {
		case MODE_KIND.ENEMY1:
			if (!one) {
				
				if (cl.gameObject.tag == "mon1turn") {
					Flip ();
					//Debug.Log ("몬스터1턴");
					justtwo++;
				}
				if (justtwo >= 100)
					one = true;
			}

			else{
				cl.isTrigger = false;
			}
			break;
		
		case MODE_KIND.ENEMY2:
			
			if (!one) {

				if (cl.gameObject.tag == "mon2turn") {
					Flip ();
					//Debug.Log ("몬스터2턴");
					justtwo++;
				}
				if (justtwo >= 50)
					one = true;
			}

			else{
				cl.isTrigger = false;
			}
			break;

		case MODE_KIND.ENEMY3:
	
			if (!one) {

				if (cl.gameObject.tag == "mon3turn") {
					Flip ();
					justtwo++;
				}
				if (justtwo >= 80)
					one = true;
			}

			else{
				Debug.Log ("이제떨어져");
				cl.isTrigger = false;
			}
			break;
		}
	}

	void Death()
	{

		moveSpeed=0f;
		anim.SetTrigger ("Die");

		//dead를 true로셋팅
		dead=true;
		DoDrop.drop = false;
		DoDrop.Update ();

		StartCoroutine (Destroymon ());
	}

	public void Flip()
	{
		dirRight = !dirRight;

		//-1을 Transform컴포넌트에 요소localScale(벡터)의 x축에 곱하자
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}

	IEnumerator Destroymon()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
	

}


