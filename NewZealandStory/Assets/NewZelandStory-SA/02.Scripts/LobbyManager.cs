using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour {

	//인벤 Panel을 가르키는 변수 선언
	public GameObject uiInven;
	//로비 화면의 버튼을 참조 할 배열 선언
	public GameObject[] lobyBtn;

	//웬만하면 이런식으로 실행 순서를 맞추기. 

	//인벤 오픈
	public void InvenOpen(){
		lobyBtn [0].SetActive (false);
		lobyBtn [1].SetActive (false);
		uiInven.SetActive (true);
	}

	//인벤 클로즈
	public void InvenClose(){
		uiInven.SetActive (false);
		lobyBtn [0].SetActive (true);
		lobyBtn [1].SetActive (true);
	}

	//사운드 중지(로딩중에 사운드 없음)
	//사운드 ui 비활성화(로딩창에 실수로도 아무것도 안나오게 하자)
	//scPlayUi 씬으로
	public void PlayGame(){
		GameObject.Find ("SoundManager").GetComponent<AudioSource> ().Stop ();
		GameObject.Find ("SoundCanvas").GetComponent<Canvas> ().enabled = false;
		//Application.LoadLevel("scPlayUi");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
