using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public int score=0;	//플레이어점수

	public PlayerCtrl PlayerCtrl;	//PlayerCtrl스크립트를 위한 레퍼런스
	public int previousScore = 0;	//이전 프레임의 점수

	//public GUIText guiText;
	public Text scoretext;

	void Awake()
	{
		//레퍼런스셋팅
		//guiText = GetComponent<GUIText> ();
		//PlayerCtrl = GameObject.Find ("Tikki1").GetComponent<PlayerCtrl> ();
		scoretext = GameObject.Find ("ScoreText").GetComponent<Text> ();
	}


	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//다음과 같이 GUIText컴포넌트 text요소에 점수를 셋팅
		//guiText.text = "SCORE: " + score;
		scoretext.text="SCORE: " + score;
		previousScore = score;//이번플레임의 점수로 PreciousScroe에 설정하자

	}

}
