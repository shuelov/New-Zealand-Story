using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCtrl : MonoBehaviour {

	public GameObject spObj;				 //GameObejct를 참조하는 spObj생성
	private bool isPlay;					 //private면 inspector에 표시되지않고 숨겨짐, 속도향상
	public Text tx;
	public Sprite[] spimgs;					 //스프라이트 담을 수 있는 배열 선언
	public Image spAnim;					 //image 컴포넌트. sprite가르킬 수 있는 참조 담을 수 있음.즉 스프라이트 연결 가능 변수
	int spimgcount;							 //이미지 애니메이션을 위한 변수
	float animTime;							 //애니메이션 속도 컨트럴 변수 
	//카드
	public GameObject scroll; 				 //->ScrollContain 게임 오브젝트 연결
	public GameObject[] btnCard;			 //->Button 객체 두개를 연결 할 배열 선언
	public GameObject bombPreFab;			 //->PreFab 게임오브젝트 연결
	bool scrollPlay; 					  	 //->bool형 변수 추가
	 
	void Start()
	{
		//spimgs = new Sprite[5];            //스프라이트 담는 배열 크기 여기서 조정할 수도 있지만 inspector에서 바로 입력함.
		spAnim.sprite=spimgs[0];
	}

	public void Play()
	{
		//Destroy(gameObject); //주석으로 제외처리
		//Destroy (this.gameObject);

		if (!isPlay) 						//만약 활성화 중이 아닐 경우
		{
			scroll.SetActive(false);		//->scroll 객체 안에 활성화 관련 함수인 SetActive()를 호출하면서 인자로 false을 전달(애니메이션 화면(sprite보여주는 다른 play가동 중 일때) 카드 안보이게 함)
			btnCard [0].SetActive (false);  //->배열로 선언된 참조의 게임오브젝트(버튼)를 비활성화(카드 play를 위한 버튼 안보이게 함)
			btnCard [1].SetActive (false);

			isPlay = true;
			spObj.SetActive (true); //연결되는 게임 오브젝트 체크 활성화
			tx.text="STOP";
		} 

		else  								//활성화 중일 경우 
		{
			btnCard [0].SetActive (true);	//->스크롤 카드 버튼을 활성화
			btnCard [1].SetActive (true);	//->스크롤 카드 추가버튼을 활성화

			isPlay = false;
			spObj.SetActive (false);		//연결되는 게임 오브젝트 체크 비활성화
			tx.text="PLAY";
		}

	}

	public void Btn_Card(){   				//->해당 버튼을 클릭시 화면에 카드 스크롤 보였다 안보였다 함
		if (!scrollPlay) {
			scrollPlay = true;				//->scroll이 활성화 되었으니 scrollPlay는 true;
			scroll.SetActive (true);		//->scroll객체 안에 활성화 관련함수인 SetActive()를 호출하면서 인자로 true을 전달
		} else {
			scrollPlay = false;				//->scroll이 비활성화되었으니 scrollPlay는 false;
			scroll.SetActive (false);		//->scroll객체 안에 활성화 관련함수인 SetActive()를 호출하면서 인자로 false을 전달
		}
	}

	public void Btn_CardUp(){
		//->Instantiate(게임오브젝트, 생성위치, 생성할 객체의 각도)를 인자로 전달.: 객체가 clone(복제로) 동적생성됨.
		//리턴되어 오는값은 데이터형이 Object형으로서 as GameObject으로 형변환
		GameObject bomb = Instantiate (bombPreFab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		bomb.transform.parent=scroll.transform;	//bomb변수가 참조하는 객체안에transform안에 parent(부모객체를 가르킴)에다가 scroll참조가 가르키는 객체의 transform의 참조값을 연결.

	}


	void Update()
	{
		if (isPlay) 
		{
			if (Time.time > animTime) 
			{
				spimgcount += 1;

				if (spimgcount > 4) 
				{
					spimgcount = 0;
				}

				spAnim.sprite = spimgs [spimgcount];
				animTime = Time.time + 0.3f;
			}
		}
	}


}
