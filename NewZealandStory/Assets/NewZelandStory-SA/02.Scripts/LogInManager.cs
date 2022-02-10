using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInManager : MonoBehaviour {
	//변수 생성
	public GameObject uiSignUp;//회원가입 다이얼로그(게임오브젝트)
	public GameObject LobbyBtn;//화면 터치하라는 버튼

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SignInUpOpen(){
		LobbyBtn.SetActive (false);//화면 터치하라는 버튼 비활성
		uiSignUp.SetActive (true);//회원가입 다이얼로그(게임오브젝트) 활성화
	}
	public void SignInUpClose(){
		uiSignUp.SetActive (false);//회원가입 다이얼로그(게임오브젝트) 활성화
	}

	//생성버튼누르면 메인화면으로 이동. 생성버튼에 이 함수 연동
	public void ToMain(){
		//GameObject.Find ("SoundManager").GetComponent<AudioSource> ().Stop ();//로비 음악 정지
		//GameObject.Find ("SoundCanvas").GetComponent<Canvas> ().enabled = false;//사운드 캔버스 비활성
		GameObject.Find ("SignInCanvas").GetComponent<Canvas> ().enabled = false;//회원가입 캔버스 비활성
		//Application.LoadLevel("NS-main");//메인화면으로 이동
	}
}
