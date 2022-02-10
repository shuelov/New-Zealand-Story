using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour {

	public GameObject[] Rides;//탈것들
	public MonPig Pig;//돼지몬스터 스크립트. 죽일때
	public PlayerCtrl2 tikkyscp;//티키 스크립트. 탈때

	public Transform pos_monpig;//돼지 위치
	public bool pigdead;//돼지 죽었나

	public Transform pos_monbird;//새 위치
	public bool birddead;//새 죽었나

	public Transform Tikki;//티키 위치
	public bool Tikkion;//티키 탑승

    //public Transform pos_rat;//탈것(쥐) 위치
    //public Transform pos_duck;//탈것(오리) 위치
    public Transform pos;


	void Awake()
	{
		//레퍼런스셋팅
		Pig=GameObject.Find ("monpig").GetComponent<MonPig> ();
		tikkyscp=GameObject.Find ("Tikki2").GetComponent<PlayerCtrl2> ();
		pos_monpig = GameObject.Find ("monpig").GetComponent<Transform> ();
		//pos_monbird= GameObject.Find ("monbird").GetComponent<Transform> ();
		Tikki=GameObject.Find ("Tikki2").GetComponent<Transform> ();
        //pos_rat = Rides[0].transform;
        //pos_duck = Rides[1].transform;
        pos = GetComponent<Transform>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!pigdead) 
		{
			//처음엔 돼지가 타고있음
			pos.position = new Vector3 (pos_monpig.position.x, pos_monpig.position.y + 0.1f, pos_monpig.position.z);
		}

		if (Tikkion && tikkyscp.geton) {
			//티키 탑승
			pos.position = new Vector3(Tikki.position.x,Tikki.position.y+0.1f,Tikki.position.z);

			if (Input.GetButtonDown ("Vertical")) 
			{
				// down 누르면 티키 따라다니지 않게. 탈 것에서 내리기
				Tikkion = false;
				tikkyscp.geton = false;
			}
		}

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//플레이어가 쏜 화살이나 폭탄에 맞으면 탄 녀석 죽음
		if (col.gameObject.tag == "Bomb" || col.gameObject.tag == "Arrow") {
			Pig.Death ();
			pigdead=true;
		}

		if (pigdead || birddead) {
			if (col.gameObject.tag == "Player") {
				Tikkion = true;
			}
		}
	}
}
