using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//현재 스크립트에서 넓게는 현재 게임오브젝트에서 반드시 필요로하는 컴포넌트를 Attrivute로 명시하여 해당 컴포넌트의 자동생성 및 삭제되는 것을 막는다.
[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour {

	//오디오 클립 저장 배열 선언
	public AudioClip[] soundFile;

	public float soundVolume = 1.0f;
	public bool isSoundMute = false;

	public Slider sl;

	public Toggle tg;

	public AudioSource audios;

	public GameObject Sound;//SoundManager에 연결
	public GameObject PlaySoundBtn;//SoundManager에 연결. SoundManager의 SoundUiOpen()에 연동할 것
 
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		//Application.LoadLevel ("scLobby");

	}

	// Use this for initialization
	void Start () {
		soundVolume = sl.value;
		isSoundMute = tg.isOn;
		PlaySoundBtn.SetActive (true);
		audios = GetComponent<AudioSource>();
		AudioSet();
		//게임로드
		LoadData();
	}
	
	// Update is called once per frame
	void Update() {
	}

	public void SetSound(){
		soundVolume = sl.value;
		isSoundMute = tg.isOn;
		AudioSet();
	}

	void AudioSet(){
		audios.volume = soundVolume;
		audios.mute = isSoundMute;
	}

	public void SoundUiOpen(){
		Sound.SetActive (true);
		PlaySoundBtn.SetActive (false);
	}

	public void SoundUiClose(){				//SoundTg안에 SoundBtn에 연동할 것
		Sound.SetActive (false);
		PlaySoundBtn.SetActive (true);
		SaveData ();
	}

	//스테이지 시작시 호출되는 함수
	public void PlayBackground( int stage)
	{
		//AudioSource의 사운드 연결
		audios.clip = soundFile [stage - 1];
		//AudioSource 셋팅
		AudioSet ();
		//사운드 플레이. Mute설정시 사운드 안나옴
		audios.Play ();
	}

	//사운드 공용함수 정의(어디서든 동적으로 사운드 게임오브젝트 생성)
	public void PlayEffect(Vector3 pos, AudioClip sfx)
	{
		//Mute옵션 설정시 이 함수를 바로 빠져나가자
		if (isSoundMute) {
			return;
		}


		//게임오브젝트의 동적생성
		GameObject _soundObj=new GameObject("sfx");
		//사운드발생위치 지정
		_soundObj.transform.position = pos;
		//생성한 게임오브젝트에  AdioSource컴포넌트를 추가하자
		AudioSource _audioSource=_soundObj.AddComponent<AudioSource>();
		// AudioSource 속성을 설정
		//사운드 파일을 연결하자
		_audioSource .clip =sfx;
		//설정되어있는 볼륨을 적용시키다. 즉  soundVolume 으로 게임전체 사운드 볼륨 조정
		_audioSource.volume=soundVolume;
		//사운드 3d 셋팅에 최소 범위를 설치하자
		_audioSource.minDistance=15.0f;
		//사운드 3d 셋팅에 최대 범위를 설정하자
		_audioSource.maxDistance=30.0f;

		//사운드를 실행시키자
		_audioSource.Play();

		//모든 사운드가 플레이 종료되면 동적 생성된 게임오브젝트 삭제
		Destroy(_soundObj, sfx.length+0.02f);
	

	}
	//게임 사운드 데이터 저장
	public void SaveData(){
		PlayerPrefs.SetFloat ("SOUNDVOLUME", soundVolume);
		//Playerprefs클래스 내부 함수에는 bool형을 저장해주는 함수가 없다.
		//bool형 데이터는 형변환을 해야 PlayerPrefs.SetInt()함수를 사용가능하다
		PlayerPrefs.SetInt ("ISSOUNDMUTE", System.Convert.ToInt32 (isSoundMute));
	}

	//게임사운드데이터 불러오기
	//바로 사운드 UI슬라이드와 토글에 적용하자
	public void LoadData()
	{
		sl.value = PlayerPrefs.GetFloat ("SOUNDVOLUME");
		//int형 데이터는 bool형으로 형변환
		tg.isOn = System.Convert.ToBoolean (PlayerPrefs.GetInt ("ISSOUNDMUTE"));

		//첫 세이브시 설정-> 이 로직없으면 첫 시작시 사운드 볼륨 0
		int isSave = PlayerPrefs.GetInt ("ISSAVE");
		if (isSave == 0) {
			sl.value = 1.0f;
			tg.isOn = false;

			//첫 세이브는 soundVolume=1.0f; isSoudnMute=false;이 디폴트값으로 저장된다.
			SaveData ();
			PlayerPrefs.SetInt ("ISSAVE", 1);
		}

	}

}
