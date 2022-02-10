using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {

	private bool paused =false;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.P)) 
		{
			paused = !paused;
		}
		//Time.timeScale는 float형으로 1값을 기준으로 유니티 엔진이 이값을 참조하여 게임 전체적인 속도를 제어(0이면 게임이 일시정지)
	if(paused)
			Time.timeScale=0;
	else
			Time.timeScale=1;
	}
}
