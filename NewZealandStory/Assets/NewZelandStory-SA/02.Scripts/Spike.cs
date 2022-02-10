using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {


	private PlayerLife playerLife;	

	void Awake()
	{
		playerLife = GameObject.Find("Tikki1").GetComponent<PlayerLife>();
	}


	void OnCollisionEnter2D (Collision2D col)
	{
		// 만약 충돌한 gameobject가 몹이거나 몹 무기라면 
		if (col.gameObject.tag == "Player") {

			playerLife.kill ();

		}
	}

}
