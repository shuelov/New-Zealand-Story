using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCtrl2 : MonoBehaviour {
	public GameObject uii;
	private Animator anim;					// Player 객체의 Animator component 를 위한 Reference(참조) 이다

	// 인스펙터에 노출 안됨 
	[HideInInspector]
	public bool dirRight = true;				// 플레이어의 현재 바라보는 방향을 알기 위함 

	[HideInInspector]
	public bool jump = false;	   			// 플레이어가 현재 점프중인지 아닌지 알기 위함 
	public float jumpForce = 500f; 		// 플레이어가 점프를 할때의 추가되는 힘의 양 
	public float v;                               // 점프 위치

	public bool jumponce;
    public float h;                             //  좌우 이동 값 받기 위함

    public bool grounded = false;			// 플레이어가 땅에 있는지 아닌지 구별을 위한 변수
	public bool obstacle = false;			// 플레이어가 머리 위쪽 벽돌에 점프했을때 닿았는지
	public bool groundedup = false;		// 플레이어가 떠있는 벽돌위에 착지했는지 아닌지 구별을 위한 변수
	public bool inwater = false;				// 플레이어가 물속인지
	public bool swimcheck=true;
	public bool jjumping = false;

	private Transform groundCheck;	 	//만약 플레이어가 땅에 있을때 position을 marking 할 곳
	public Transform obstacleCheck;
	public Transform waterCheck;

	public float moveForce = 365f;			// 플레이어의 왼쪽 오른쪽 이동을 위한 추가되는 힘의 양	
	public float maxSpeed = 5f;				// 가장 빠르게 x 축 안에서 플레이어가 이동 할수있는 최고 스피드

	public RectTransform stick;

	public bool geton;							// 티키가 탈것에 올랐는지 아닌지. Ride스크립트에서 필요

	public Collider2D Door;

	private Rigidbody2D rig;

	public ToLastStage hidden;

	public BombThrow activebomb;

	public bool settransform=false;

	public float sp;								//time speed

	private Riderat ride;
	public Transform Ratttt;
	private Rigidbody2D ratrigid;
	public bool OnNOn; 
	private PlayerCtrl2 TTKKIscript;
	public bool Really=false;



	void Awake()
	{
		//uifalse ();
		// 레퍼런스(참조)들을 셋팅.
		groundCheck = transform.Find("groundCheck");
		obstacleCheck = transform.Find ("obstacleCheck");

		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D> ();
		activebomb=GameObject.Find ("BombThrow").GetComponent<BombThrow> ();
		TTKKIscript = GetComponent<PlayerCtrl2>();

	}

	public void Start()
	{		//티키가 탈것 탔을 때
		if (OnNOn)
		{
			//Ratttt = GameObject.Find("riderat").GetComponent<Transform>();
			//Debug.Log(Ratttt);
			anim.SetFloat("Speed",0);
			anim.SetBool("swim",false);
			anim.SetBool ("swimjump", false);
			anim.SetTrigger ("back");
			TTKKIscript.enabled = false;
			anim.enabled = false;
			//Debug.Log ("HMM");
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "HelpingOut") 
		{
			if (swimcheck)
			{
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 60);
			}
		}

		if (c.gameObject.tag == "BossDoor") {
			SceneManager.LoadScene ("5.NS-Boss");
		}
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if (c.gameObject.tag == "water")
		{
			StartCoroutine(Waterornot());
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		swimcheck = false;
		anim.SetBool("swimjump", false);
		anim.SetBool("swim", false);
		Back();
	}


	IEnumerator Waterornot()
	{
		rig.gravityScale = 0.3f;
		jumpForce = 60f;
		anim.SetBool ("swim",true);
		yield return new WaitForSeconds (1.5f);
		swimcheck = true;
	}

	void Back()
	{
		rig.gravityScale = 2.5f;
		jumpForce = 500f;
		anim.SetTrigger ("back");
	}
	#if UNITY_STANDALONE_WIN

	void uifalse()
	{
		uii.SetActive (false);
	}


	void FixedUpdate ()
	{

	//Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
	h = Input.GetAxis("Horizontal");
        //h = UltimateJoystick.GetHorizontalAxis ("BangBang3");
       // Debug.Log(Input.GetAxis("Horizontal"));
	// animator 컴포넌트에 parameter(매개변수)인 Speed에 horizontal(수평) 입력값의 절대값(Mathf.Abs())을 셋팅한다.
	anim.SetFloat("Speed", Mathf.Abs(h*0.5f));

	//만약 플레이어의 바라보는 방향이 바뀌거나 혹은 maxSpeed에 아직 도달하지 않을때 ( h(-1.0f~1.0f)는 velocity.x를 다르게 표시한다)
	if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
	//플레이어 객체에 힘을 가한다.
	GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

	// 만약에 플레이어의 수평 속도가 maxSpeed 보다 크면 
	if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
	//플레이어의 velocity(속도)를 x축방향으로 maxSpeed 로 셋팅해줘라 또한 기존 rigidbody2D.velocity.y 도 셋팅 해 줘야 한다.
	// Mathf.Sign() 는 매개변수를 참조해서 1 또는 -1(float)을 반환  
	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

	// 왼쪽을 바라볼때 오른쪽으로 이동하게 입력했다면 뒤집기
	if (h > 0 && !dirRight)
	Flip();
	// 오른쪽을 바라볼때 왼쪽으로 이동하게 입력했다면 뒤집기
	else if (h < 0 && dirRight)
	Flip();

	Jump ();
	}

	void Update()
	{


	// 플레이어 position 으로부터 groundcheck position 까지 linecast(두 점을 잇는 선을 그림)할때 
	// 만약 충돌한 어떤객체가 Ground layer 라면 현재 플레이어는 땅에 있는거다.
	grounded = Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Ground"));	  // 완전 바닥
	groundedup= Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Obstacle")); // 공중에 떠있는 벽돌 위

	if (!geton)
	{
	anim.SetBool("geton", false);
	geton = Physics2D.Linecast(groundCheck.position, transform.position, 1 << LayerMask.NameToLayer("Ride")); //탈것 위로 올라가면
	}



	v = Input.GetAxis("Vertical");//수직 값 받음(-1~1)사이
	//v = UltimateJoystick.GetVerticalAxis ("BangBang3"); //조이스틱 


	//수영중이 아닐때	
	if (!swimcheck) {
	//	if (!geton) {
	// 일반점프
	// 만약 점프 버튼을 눌렀를때 플레이어가 땅에 있었다면 플레이어는 점프 할 수있다.
	if (Input.GetButtonDown("Vertical") &&grounded && jumponce == false ||Input.GetButtonDown("Vertical")&&groundedup && jumponce == false) {

	jumponce = true;
	jump = true;
	} 
	}

	//수영중일때
	if (swimcheck) {
	if (Input.GetButton ("Vertical")&& jumponce == false /*|| Input.GetButton ("Vertical") && jump == false*/) {
	//Debug.Log ("122221");
	jumponce = true;
	jump = true;
	}

	if (Time.time > sp) {
	jumponce = false;
	sp = Time.time + (7.0f * Time.deltaTime); //deltatime(프레임당 걸리는 시간)
	}

	}

	}
	#endif

	#if UNITY_ANDROID

	void FixedUpdate ()
	{

		//Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
		h = Input.GetAxis("Horizontal");
		h = UltimateJoystick.GetHorizontalAxis ("BangBang3");

		// animator 컴포넌트에 parameter(매개변수)인 Speed에 horizontal(수평) 입력값의 절대값(Mathf.Abs())을 셋팅한다.
		anim.SetFloat("Speed", Mathf.Abs(h));

		//만약 플레이어의 바라보는 방향이 바뀌거나 혹은 maxSpeed에 아직 도달하지 않을때 ( h(-1.0f~1.0f)는 velocity.x를 다르게 표시한다)
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			//플레이어 객체에 힘을 가한다.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		// 만약에 플레이어의 수평 속도가 maxSpeed 보다 크면 
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			//플레이어의 velocity(속도)를 x축방향으로 maxSpeed 로 셋팅해줘라 또한 기존 rigidbody2D.velocity.y 도 셋팅 해 줘야 한다.
			// Mathf.Sign() 는 매개변수를 참조해서 1 또는 -1(float)을 반환  
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		// 왼쪽을 바라볼때 오른쪽으로 이동하게 입력했다면 뒤집기
		if (h > 0 && !dirRight)
			Flip();
		// 오른쪽을 바라볼때 왼쪽으로 이동하게 입력했다면 뒤집기
		else if (h < 0 && dirRight)
			Flip();

		Jump ();
	}

	void Update()
	{


		// 플레이어 position 으로부터 groundcheck position 까지 linecast(두 점을 잇는 선을 그림)할때 
		// 만약 충돌한 어떤객체가 Ground layer 라면 현재 플레이어는 땅에 있는거다.
		grounded = Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Ground"));	  // 완전 바닥
		groundedup= Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Obstacle")); // 공중에 떠있는 벽돌 위

		if (!geton)
		{
			anim.SetBool("geton", false);
			geton = Physics2D.Linecast(groundCheck.position, transform.position, 1 << LayerMask.NameToLayer("Ride")); //탈것 위로 올라가면
		}



		//v = Input.GetAxis("Vertical");//수직 값 받음(-1~1)사이
		v = UltimateJoystick.GetVerticalAxis ("BangBang3"); //조이스틱 


		//수영중이 아닐때	
		if (!swimcheck) {
		//	if (!geton) {
				// 일반점프
				// 만약 점프 버튼을 눌렀를때 플레이어가 땅에 있었다면 플레이어는 점프 할 수있다.
				if (/*Input.GetButtonDown("Vertical") &&*/ (grounded && v >= 0.5f && jumponce == false )|| /*Input.GetButtonDown("Vertical")&&*/(groundedup && v >= 0.5f && jumponce == false)) {

					jumponce = true;
					jump = true;
				} 
		}

		//수영중일때
		if (swimcheck) {
			if (/*Input.GetButton ("Vertical") &&*/v >= 0.5f && jumponce == false /*|| Input.GetButton ("Vertical") && jump == false*/) {
				//Debug.Log ("122221");
				jumponce = true;
				jump = true;
			}

			if (Time.time > sp) {
				jumponce = false;
				sp = Time.time + (15.0f * Time.deltaTime); //deltatime(프레임당 걸리는 시간)
			}

		}

	}
	#endif
	void Jump()
	{

			//수영중이 아닐때
			if(!swimcheck)
			{

					obstacle=Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Obstacle"));  
					jjumping=Physics2D.Linecast(transform.position, obstacleCheck.position, 1 << LayerMask.NameToLayer("Water"));

					// 만약 플레이어가 점프를 한다면
					if (jump)
					{
						if (!jumponce) {
							return;
						}

						anim.SetTrigger("Jump");

						//플레이어게게 수직 힘이 가해진다
						
						GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

						if (obstacle)
						{
							StartCoroutine ("Changebool");
							obstacle=false;
						}

						// Update에서 조건이 만족하여 점프상태가 될때까지 확실하게 플레이어가 다시 점프를 못하게 만들어라 
						jump = false;
						v = 0f;
						grounded = false;
						groundedup = false;
					}
			}

			//수영중일때
			else if (swimcheck) 
			{
					// 만약 플레이어가 점프를 한다면
					if (jump) 
					{
						if (!jumponce)
							return;

						anim.SetBool("swimjump",true);

						//플레이어게게 수직 힘이 가해진다
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
					
						// Update에서 조건이 만족하여 점프상태가 될때까지 확실하게 플레이어가 다시 점프를 못하게 만들어라 
						jump = false;
						v = 0f;
						grounded = false;

					}
			}


	}

	//벽돌 통과해서 위로 점프해야 할 때 잠시 trigger true했다가 일정시간 뒤 false해서 위 벽돌에 착지하게 
	IEnumerator Changebool()
	{
		if (!swimcheck) 
		{
			Collider2D[] cols = GetComponents<BoxCollider2D> ();

			foreach (Collider2D c in cols)
			{
				//Debug.Log ("점프중player trigger true");
				if (c.gameObject.tag != "Wall")
				{
					if (c.tag == "Player")
						c.isTrigger = true;	
				}
			}

			// 아래 jumponce 앞에 '//'해제했음
			jumponce = false;
			yield return new WaitForSeconds (0.1f);
			foreach (Collider2D c in cols) 
			{
			//Debug.Log ("다시 player trigger false");
				if (c.tag == "Player")
					c.isTrigger = false;	
			}
		}

	}


	void OnCollisionEnter2D (Collision2D col)
	{

		if (!swimcheck) 
		{
			Collider2D[] cols = GetComponents<BoxCollider2D> ();

			//만약 캐릭터와 바닥에 있을때 닿는 충돌체가 Ground이거나 Obstacle이면
			if (/*grounded ||*/ groundedup||obstacle) {
				foreach (Collider2D c in cols) {
					//Debug.Log ("땅에서있어");
					if (c.tag == "Player")
						c.isTrigger = false;	
					jumponce = false;
				}
			}

			if (grounded||obstacle) 
			{
			jumponce = false;
			}
	
			//만약 캐릭터와 ground아닐때 닿는 충돌체가 Ground이면
			if (/*col.gameObject.tag == "Ground" && !grounded ||*/ col.gameObject.tag == "Obstacle" && !grounded || col.gameObject.tag == "mon1turn" && !grounded) {
				foreach (Collider2D c in cols)
				{
					//Debug.Log ("땅에떨어짐");
					if (c.tag == "Player")
						c.isTrigger = false;	
					jumponce = false;
				}

			}
		}

		if (col.gameObject.tag == "Point")
		{
			col.transform.GetComponentInParent<Item>().pluspoint();
			//Debug.Log ("callpluspoint");
			if (col.transform.GetComponentInParent<Item>().num == 5)
			{
				GameObject.Find("ArrowShoot").SetActive(false);
				activebomb.enabled = true;
			}
		}

	}

	// 캐릭터의 현재 방향을 바꿔주는 함수 
	void Flip ()
	{
		//플레이어의 바라보는 방향을 바꾸자 
		dirRight = !dirRight;

		//플레이어의 local scale x에 -1을 곱하자
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// 플레이어 게임오브젝트를 탈것의 자식으로 넣기
	public void TikkiSetparent()
	{
		anim.SetFloat ("Speed", 0f);
		transform.SetParent(Ratttt, false);
		//// 티키 탑승
		transform.position = new Vector3(Ratttt.position.x, Ratttt.position.y + 0.3f, Ratttt.position.z);
		transform.localScale = new Vector3(-0.15f, 0.15f, 1);
		Ratttt.localScale = new Vector3(4, 3, 1);
		rig.simulated = false;

	}

	public void BeAlone()
	{
		if (Really)
		{
			anim.enabled = true;
			//부모 자식관계 차단
			this.transform.parent = null;
			Vector3 theScale = transform.localScale;
			rig.simulated = true;
			// localScale 조정. rigidbody2d simulated true로
			OnNOn = false;
			Really = false;
			StartCoroutine(NoRideOnlyTikki());
		}
	}

	//몇초(딜레이) 부딪히면 다시 탈 수 있게
	IEnumerator NoRideOnlyTikki()
	{
		yield return new WaitForSeconds(3);
		// 탈 것에서 내리기
		geton = false;
		Vector3 theScale = transform.localScale;//
		//theScale.x *= -1;//
		ride = GameObject.Find("riderat").GetComponent<Riderat>();
		ride.Tikkion = false;

	}


}


