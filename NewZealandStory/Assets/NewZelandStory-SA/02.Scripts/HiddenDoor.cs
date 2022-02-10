using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour {


	public int HP=10;//문 열리기 까지 10번 쳐야함

	private Animator anim;//animator 컴포넌트를 위한 레퍼런스
	public bool open = false;//열렸나?


	void Awake()
	{
		//레퍼런스들의 셋팅
		anim=GetComponent<Animator>();

	}

	public void DoorDamaged()
	{
		//비밀의 문 있는 곳 때리면 1만큼 줄인다
		HP--;
	}

	void FixedUpdate()
	{

		if (HP <= 0 && !open)
			//DoorOpen()함수 호출
			DoorOpen ();
	}

	void DoorOpen()
	{
		anim.SetTrigger ("Open");
		GameObject.Find ("HiddenDoorflame").SetActive (true);
		GameObject.Find ("HiddenDoorBase").SetActive (true);
		open=true;

	}

}

