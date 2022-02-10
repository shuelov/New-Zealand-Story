using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootmiddle : MonoBehaviour {

	private PlayerCtrl2 playerCtrl;						//PlayerCtrl script를 위한 Reference

	public Rigidbody2D weaponprefab;				//활쏘는 prefab연결

	public float weaponSpeed=20f;					//화살나가는 스피드

	public bool fire;										//공격 확인

	public float l_x;
	public float l_y;

	public float r_x;
	public float r_y;

	void Awake()
	{
		playerCtrl=transform.root.GetComponent<PlayerCtrl2>();
	}


	void Update ()
	{
		Fire ();

		if (fire)
		{

			Vector3 theScale = weaponprefab.transform.localPosition;

			//만약 플레이어가 오른쪽 방향이라면
			if (playerCtrl.dirRight)
			{
				r_x = theScale.x;
				r_y = theScale.y;
				//오른쪽 방향으로 화살을 생성하고 오른쪽으로 화살의 속도를 셋팅
				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (weaponSpeed, 0);
				//발사

			} else {
				l_x = theScale.x;
				l_y = theScale.y;
				//그렇지 않으면 왼쪽 방향으로 화살을 생성하고 왼쪽으로 화살의 속도를 셋팅
				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (-weaponSpeed, 0);
				//발사
			}

		}

		fire = false;
	}


//	void FixedUpdate()
//	{
//		Fire ();
//	}

	public void FireButton()
	{
		fire = true;
	}

	public void Fire()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			fire = true;
		}

		if (!fire)
			return;

	}


}

