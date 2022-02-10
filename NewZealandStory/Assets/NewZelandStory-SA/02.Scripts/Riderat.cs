using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Riderat : MonoBehaviour {

	public GameObject[] Rides;						 //탈것들
	public MonPig Pig;								//돼지몬스터 스크립트. 죽일때
	public PlayerCtrl2 tikkyscp;                    //티키 스크립트. 탈때
	public PlayerLife2 tikkyscplife;

	public Transform pos_monpig;             	  //돼지 위치
	public bool pigdead;                           //돼지 죽었나

	public bool Tikkion;                           //티키 탑승
	public bool Tikkioff = false;                 //티키 하차

	public Rigidbody2D Ratrigid2d;

	private Animator anim;                     	 // Player 객체의 Animator component 를 위한 Reference(참조) 이다
	private Transform RatTrans;
	private Transform groundCheck;        		 // Ride의 groundcheck
	public bool IsGround;                        // 바닥에 착지한 상태인지
	public bool IsGround2;                     	 // 공중에 떠있는 벽돌 위에 착지한 상태인지
		
	// 인스펙터에 노출 안됨 
	[HideInInspector]
	public bool dirRight = true;                  // 현재 바라보는 방향을 알기 위함 

	public float moveForce = 2;                	 // 왼쪽 오른쪽 이동을 위한 추가되는 힘의 양	
	public float maxSpeed = 0.5f;             	 // 가장 빠르게 x 축 안에서 이동 할수있는 최고 스피드

	public float h;
	public float v;
	public bool down=false;


	private Transform deadposition;                //죽은 자리 (아이템 떨굴 자리)
	[HideInInspector]
	public float lastpositionX;
	[HideInInspector]
	public float lastpositionY;

	public BombThrow activebomb;

	void Awake()
	{
		//레퍼런스셋팅
		Pig = transform.root.GetComponent<MonPig>();
		tikkyscp = GameObject.Find("Tikki2").GetComponent<PlayerCtrl2>();
		tikkyscplife = GameObject.Find("Tikki2").GetComponent<PlayerLife2>();
		pos_monpig = transform.root.GetComponent<Transform>(); ;
		//pos_monbird= GameObject.Find ("monbird").GetComponent<Transform> ();
		RatTrans = GetComponent<Transform>();
		groundCheck = transform.Find("groundCheck");
		deadposition = GetComponent<Transform>();

		activebomb = GameObject.Find("BombThrow").GetComponent<BombThrow>();
	}
	#if UNITY_STANDALONE_WIN
	void Update()
	{

	//Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
	h = Input.GetAxis("Horizontal");
	//	h = UltimateJoystick.GetHorizontalAxis("BangBang3");
	v = Input.GetAxis("Vertical");
	//	v = UltimateJoystick.GetVerticalAxis("BangBang3");

	IsGround = Physics2D.Linecast(groundCheck.position, transform.position, 1 << LayerMask.NameToLayer("Ground"));    // 완전 바닥
	IsGround2 = Physics2D.Linecast(groundCheck.position, transform.position, 1 << LayerMask.NameToLayer("Obstacle")); // 공중에 떠있는 벽돌 위

	if(pigdead){
	if (Tikkion && tikkyscp.geton) {

	if(down)
	{
	//다시 tikki스크립트 활성화하기
	tikkyscp.enabled = true;
	tikkyscp.Really = true;
	tikkyscp.BeAlone();
	Tikkioff = false;				
	//
	Tikkion = false;
	down = false;
	}

	if(Tikkion)
	{
	//전후좌우 이동 방향 벡터 계산
	Vector3 moveDir = (Vector2.up * v) + (Vector2.right * h);

	//Translate(이동 방향 * Time.deltaTime * 변위값 * 속도, 기준좌표) 이동!
	RatTrans.Translate(moveDir * Time.deltaTime * moveForce, Space.Self);
	}
	Ratrigid2d = GetComponent<Rigidbody2D>();
	}


	if (Input.GetButtonDown("Fire3"))
	{
	ResetRide();
	Destroy (gameObject);
	}


	if (!tikkyscplife.tikidead)
	{
	lastpositionX = deadposition.position.x;
	lastpositionY = deadposition.position.y;
	}
	}


	else if (!pigdead)
	{
	//처음엔 돼지가 타고있음
	transform.position = new Vector3(pos_monpig.position.x, pos_monpig.position.y + 0.1f, pos_monpig.position.z);
	}

	}

	void FixedUpdate()
	{ 
	if (Tikkion && tikkyscp.geton)
	{
	//탑승중
	//if (tikkyscp.OnNOn)
	if(Tikkion)
	{

	// 왼쪽을 바라볼때 오른쪽으로 이동하게 입력했다면 뒤집기
	if (h > 0 && !dirRight)
	Flip();
	// 오른쪽을 바라볼때 왼쪽으로 이동하게 입력했다면 뒤집기
	else if (h < 0 && dirRight)
	Flip();

	//Debug.Log(v);
	}
	}

	if (tikkyscplife.tikidead)
	{
	StartCoroutine(Revive());
	}

	}
	#endif

	#if UNITY_ANDROID
	void Update()
	{

		//Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
		h = Input.GetAxis("Horizontal");
		h = UltimateJoystick.GetHorizontalAxis("BangBang3");
		v = Input.GetAxis("Vertical");
		v = UltimateJoystick.GetVerticalAxis("BangBang3");

		IsGround = Physics2D.Linecast(groundCheck.position, transform.position, 1 << LayerMask.NameToLayer("Ground"));    // 완전 바닥
		IsGround2 = Physics2D.Linecast(groundCheck.position, transform.position, 1 << LayerMask.NameToLayer("Obstacle")); // 공중에 떠있는 벽돌 위


		if (!pigdead)
		{
			//처음엔 돼지가 타고있음
			transform.position = new Vector3(pos_monpig.position.x, pos_monpig.position.y + 0.1f, pos_monpig.position.z);
		}

		else if (Tikkion && tikkyscp.geton) {

			//키보드버전
			//if (Input.GetButtonDown("Down") && IsGround || Input.GetButtonDown("Down") && IsGround2)
			if(down)
			{
			//	//다시 tikki스크립트 활성화하기
				tikkyscp.enabled = true;
				tikkyscp.Really = true;
				tikkyscp.BeAlone();
				Tikkioff = false;				
				//
				Tikkion = false;
				down = false;
			}

			////폰버전 플레이어 하차
			//if (Tikkioff)
			//{
			//	//다시 tikki스크립트 활성화하기
			//	tikkyscp.enabled = true;
			//	tikkyscp.Really = true;
			//	tikkyscp.BeAlone();
			//	Tikkioff = false;
			//}

			//탑승중
			//if (tikkyscp.OnNOn)
			if(Tikkion)
			{
				//전후좌우 이동 방향 벡터 계산
				Vector3 moveDir = (Vector2.up * v) + (Vector2.right * h);

				//Translate(이동 방향 * Time.deltaTime * 변위값 * 속도, 기준좌표) 이동!
				RatTrans.Translate(moveDir * Time.deltaTime * moveForce, Space.Self);
			}
			Ratrigid2d = GetComponent<Rigidbody2D>();
		}

		//RoomItem의  Button 컴포넌트에 클릭 이벤트를 '동적'으로 연결
		GameObject.FindGameObjectWithTag("GetOFF").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{ ResetRide(); });


	
		if (!tikkyscplife.tikidead)
		{
			lastpositionX = deadposition.position.x;
			lastpositionY = deadposition.position.y;
		}
		
	}

	void FixedUpdate()
	{ 
		 if (Tikkion && tikkyscp.geton)
		{
				//탑승중
				//if (tikkyscp.OnNOn)
			if(Tikkion)
				{

					// 왼쪽을 바라볼때 오른쪽으로 이동하게 입력했다면 뒤집기
					if (h > 0 && !dirRight)
						Flip();
					// 오른쪽을 바라볼때 왼쪽으로 이동하게 입력했다면 뒤집기
					else if (h < 0 && dirRight)
						Flip();

					//Debug.Log(v);
				}
		}

		if (tikkyscplife.tikidead)
		{
			StartCoroutine(Revive());
		}

	}
	#endif
	IEnumerator Revive()
	{
		if (tikkyscplife.Life != 0)
		{
			transform.position = new Vector3(0, 5, 0);
			yield return new WaitForSeconds(3);
			transform.position = new Vector3(lastpositionX, lastpositionY, 0);
		}
		else
			Destroy(gameObject);
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (!pigdead) {

			//플레이어가 쏜 화살이나 폭탄에 맞으면 탄 녀석 죽음
			if (col.gameObject.tag == "Bomb" || col.gameObject.tag == "Arrow") {
				transform.root.GetComponent<MonPig>().Death();
				Pig.Death();
				pigdead = true;
			}
		}

		if (Tikkion)
		{
			if (col.gameObject.tag == "Spike")
			{
				GetComponentInChildren<PlayerLife2>().kill();
				if (tikkyscplife.Life == 0)
					Destroy(gameObject, 1);
			}


			if (col.gameObject.tag == "BossDoor")
			{
				Debug.Log("doorenter");
				SceneManager.LoadScene("5.NS-Boss");
			}

		}

		if (!tikkyscp.OnNOn)
		{	
			//플레이어가 올라타면
			if (col.gameObject.tag == "Player")
			{
				if (!Tikkion&&pigdead)
				{
					Tikkion = true;
					//탑승
					//탑승
					tikkyscp.OnNOn = true;
					tikkyscp.Start();
					tikkyscp.Ratttt = GetComponent<Transform>();
					//tikkyscp.OnNOn = true;
					if (Tikkion)
					{
						GameObject.Find("MainCamera").GetComponent<FollowCamera1>().Awake();
						GameObject.Find("MainCamera").GetComponent<FollowCamera1>().rat = GetComponent<Riderat>();
						tikkyscp.TikkiSetparent();

					}
				}

				else if (!Tikkion && tikkyscp.geton)
				{
					Tikkion = true;
					//돼지 죽이기
					transform.root.GetComponent<MonPig>().Death();
					//Pig.Death();
					pigdead = true;
					//탑승
					tikkyscp.OnNOn = true;
					tikkyscp.Start ();
					tikkyscp.Ratttt = GetComponent<Transform>();
					if (Tikkion)
					{
						tikkyscp.TikkiSetparent();

					}
				}
			}
		}
		if (col.gameObject.tag == "Point")
		{
			if (Tikkion)
			{
				col.transform.GetComponentInParent<Item>().pluspoint();
				if (col.transform.GetComponentInParent<Item>().num == 5)
				{
					GameObject.Find("ArrowShoot").SetActive(false);
					activebomb.enabled = true;
					Debug.Log("callpluspoint1");
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (Tikkion)
		{
			if (c.gameObject.tag == "BossDoor")
			{
				SceneManager.LoadScene("5.NS-Boss");
			}
		}

	}
	
	public void GetOFF()
	{
		this.transform.parent = null;
	}

	// 캐릭터의 현재 방향을 바꿔주는 함수 
	void Flip()
	{
		//플레이어의 바라보는 방향을 바꾸자 
		dirRight = !dirRight;

		//플레이어의 local scale x에 -1을 곱하자
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ResetRide()
	{

		down = true;
		//	//다시 tikki스크립트 활성화하기
		tikkyscp.enabled = true;
		tikkyscp.Really = true;
		tikkyscp.BeAlone();

		Tikkioff = false;
		Tikkion = false;
		down = false;
	}

}





