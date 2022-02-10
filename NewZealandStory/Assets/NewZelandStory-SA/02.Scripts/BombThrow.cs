using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour {

	public PlayerCtrl playerCtrl;					//PlayerCtrl script를 위한 Reference

	public Rigidbody2D weaponprefab;		 //prefab연결

	public float weaponSpeed=20f;			 //스피드

	public bool fire;

	public float l_x;
	public float l_y;

	public float r_x;
	public float r_y;

	void Awake()
	{

		playerCtrl = transform.parent.GetComponent<PlayerCtrl> ();
	}

	void Update ()
	{
		Fire ();
		if (fire) {
		
			Vector3 theScale = weaponprefab.transform.localScale;

			//만약 플레이어가 오른쪽 방향이라면
			if (playerCtrl.dirRight)
			{
				theScale.x = 6f;
				theScale.y = 4.5f;

				r_x = theScale.x;
				r_y = theScale.y;

				weaponprefab.transform.localScale = theScale;

				//오른쪽 방향으로 화살을 생성하고 오른쪽으로 화살의 속도를 셋팅
				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (weaponSpeed, 0);

			}

			else if (!playerCtrl.dirRight)
			{
				//그렇지 않으면 왼쪽 방향으로 화살을 생성하고 왼쪽으로 화살의 속도를 셋팅
				theScale.x = -6f;
				theScale.y = -4.5f;

				l_x = theScale.x;
				l_y = theScale.y;

				weaponprefab.transform.localScale = theScale;

				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (-weaponSpeed, 0);
				}	

		}
		fire = false;
	}

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



