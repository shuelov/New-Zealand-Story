using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLastStage : MonoBehaviour {
	
	//private Animator anim;//animator 컴포넌트를 위한 레퍼런스
	public bool open = false;//열렸나?
	//private Rigidbody2D rigidbody2D;

	void Awake()
	{
		//레퍼런스들의 셋팅
		//anim=GetComponent<Animator>();
		DoorOpen ();
	}

	void DoorOpen()
	{
		//anim.SetTrigger ("Open");
		GameObject.Find ("HiddenDoorflame2").SetActive (true);
		GameObject.Find ("HiddenDoorBase2").SetActive (true);

		open=true;

	}

}

