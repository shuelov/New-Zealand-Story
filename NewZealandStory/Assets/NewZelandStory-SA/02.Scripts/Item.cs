using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int point;
	public GameObject pointpre;
	public Animator pointanim;
	public int num;


	//private float sp;

	public Score scscore;			//Score스크립트를 위한 레퍼런스

	void Awake()
	{
		scscore = GameObject.Find ("ScoreScript").GetComponent<Score> ();
		pointanim = GetComponent<Animator> ();

	}

	public void pluspoint()
	{
					
		//스코어 증가->스코어 애니메이션 실행->스코어 이미지(게임오브젝트) 삭제
		scscore.score+=point;
		pointanim.SetTrigger ("point");
		Destroy (gameObject);
			
		if(num != 5)
			  Instantiate (pointpre,transform.position, Quaternion.identity);

		if (num == 5)
		{
			 Instantiate (pointpre,transform.position, Quaternion.identity);
		}

			//Debug.Log ("itemgone");
			//sp = Time.time +(100.0f * Time.deltaTime); //deltatime(프레임당 걸리는 시간)
		
	}


}
