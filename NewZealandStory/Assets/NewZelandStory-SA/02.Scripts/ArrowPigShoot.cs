using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPigShoot : MonoBehaviour {
	private Animator anim;//애니메이터 트리거 shoot을 on하기위해 필요

	private MonPig monpig;//PlayerCtrl script를 위한 Reference

	public Rigidbody2D weaponprefab; //활쏘는 prefab연결

	public float weaponSpeed=20f; //화살나가는 스피드

	//public bool pressBtn=false;
	public bool fire;


	public float l_x;
	public float l_y;

	public float r_x;
	public float r_y;

	void Awake()
	{

		//Reference를 셋팅
		anim=transform.root.gameObject.GetComponent<Animator>();
		monpig=transform.root.GetComponent<MonPig>();
	}

	// Use this for initialization
	void Update ()
	{


		//pressBtn = UltimateJoystick.GetTapCount ("BoomBoom");
		if (fire) {


			//if (pressBtn) { //tab하면 shoot으로 변경. 공격

			//	fire = true;
			//}

			Vector3 theScale = weaponprefab.transform.localPosition;
			//만약 플레이어가 오른쪽 방향이라면
			if (monpig.dirRight) {

				r_x = theScale.x;
				r_y = theScale.y;
				//오른쪽 방향으로 화살을 생성하고 오른쪽으로 화살의 속도를 셋팅
				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (weaponSpeed, 0);//3D라면 Vector3로 받음. //velocity선택하면 한쪽으로 쭉 감



				//Instantiate로 생성하면 return값이 object라 형변환필요 
				//만약 Rigidbody2D로 받지않고 gameObject=ga로 받았다면 ga.GetComponent<Rigidbody2D>().velocity로 접근해야해서'.'더 쓰게됨 비추.
				//public를 사용해서 드래그드롭하면 제일 짧지만 Insepctor에 노출되는 public변수 선언은 아껴줘야함. 
			} else {

				l_x = theScale.x;
				l_y = theScale.y;
				//그렇지 않으면 왼쪽 방향으로 화살을 생성하고 왼쪽으로 화살의 속도를 셋팅
				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (-weaponSpeed, 0);
			}
				
		}
		fire = false;
	}

	void FixedUpdate()
	{
		//Fire ();
	}


	public void Fire()
	{
		//if (!fire)
		//	return;

		anim.SetTrigger ("shoot");
		Input.GetButtonDown ("Fire2");
		//GetComponent<AudioSource> ().Play ();
		//pressBtn = false;
		fire = true;
	}

}

