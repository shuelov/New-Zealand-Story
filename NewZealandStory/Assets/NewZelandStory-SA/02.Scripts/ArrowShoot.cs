using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour {

	private PlayerCtrl playerCtrl;//PlayerCtrl script를 위한 Reference

	public Rigidbody2D weaponprefab; //활쏘는 prefab연결

	public float weaponSpeed=20f; //화살나가는 스피드

	public bool fire;

	public float l_x;
	public float l_y;

	public float r_x;
	public float r_y;

	void Awake()
	{
		playerCtrl=transform.parent.GetComponent<PlayerCtrl>();
	}

	void Update ()
	{
		Fire ();
		if (fire) {

			Vector3 theScale = weaponprefab.transform.localPosition;
			//만약 플레이어가 오른쪽 방향이라면
			if (playerCtrl.dirRight) {

				r_x = theScale.x;
				r_y = theScale.y;
				//오른쪽 방향으로 화살을 생성하고 오른쪽으로 화살의 속도를 셋팅
				Rigidbody2D bulletInstance = Instantiate (weaponprefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2 (weaponSpeed, 0);//3D라면 Vector3로 받음. //velocity선택하면 한쪽으로 쭉 감
 
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

