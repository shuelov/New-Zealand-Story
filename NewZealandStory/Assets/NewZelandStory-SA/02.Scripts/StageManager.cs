using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	//스테이지
	public int stage;
	//SoundManager 컴포넌트를 연결 할 변수->인스펙터 뷰에는 꼭 필요한 변수만 노출시킴 
	public SoundManager _sMgr;

	//테스트 변수
	public AudioClip soundClip;
	private float soundTime;

	// Use this for initialization
	void Start () {

		//SoundManager게임오브젝트의 SoundManager 컴포넌트 연결
		_sMgr = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		//배경사운드 플레이
		_sMgr.PlayBackground (stage);

		//해당 게임오브젝트를 계층뷰에서 찾아 비활성화
		//GameObject.Find ("ExplainUi").SetActive (false);
		//사운드  Ui를 보이게하자. (설정중에 로딩창으로 넘어가도 이어서 보이게하자)
		//GameObject.Find ("SoundCanvas").GetComponent<Canvas> ().enabled = true;
	}


	//--------------------------------------바뀌어서 SceneManager라고 class명 사용못함(중복됨)
	////위에 추가
	//using UnityEngine.SceneManagement;

	////룸 씬으로 이동하는 코루틴 함수 (UI 버전에서 사용)
	//IEnumerator LoadStage()
	// 5.3 이후 
	//SceneManager.LoadScene(0);                                          // 로드. 
	//SceneManager.LoadScene("SceneName");
	//AsyncOperation ao = SceneManager.LoadSceneAsync(0);                 // 로드. (비동기)
	//AsyncOperation ao = SceneManager.LoadSceneAsync("SceneName");
	//-----------------------------------------------------------------------------------------


	// Update is called once per frame
	void Update () {
		if (Time.time > soundTime) {
			//3.5초마다 번개사운드 연출
			LightingSound ();
			soundTime = Time.time + 3.5f;
		}
	}

	//병렬 처리를 위한 코루틴 함수를 호출
	void LightingSound(){
		//Startcoroutine으로 Coroutine 함수 호출
		StartCoroutine (this.PlayEffectSound(soundClip));
	}
	//Effect 테스트 사운드를 Coroutine으로 생성
	IEnumerator PlayEffectSound(AudioClip _clip){
		//공용사운드 함수 호출
		_sMgr.PlayEffect (transform.position, _clip);
		yield return null;
	}
}
