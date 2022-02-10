using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Playerinwater: MonoBehaviour {

	private Animator anim;					// Player 객체의 Animator component 를 위한 Reference(참조) 이다

	//private Transform tikkiinwater;			//위치 조정 위함

	//private Transform Tikki;				//위치 가져오기 위함

	public PlayerCtrl2 TK;					//물속인지 아닌지 알기위해 bool형 가져올거

	//private Rigidbody2D rig;				//중력값 조절 위함

	// 인스펙터에 노출 안됨 
	[HideInInspector]
	public bool dirRight = true;			// 플레이어의 현재 바라보는 방향을 알기 위함 


	[HideInInspector]
	public bool jump = false;	   			// 플레이어가 현재 점프중인지 아닌지 알기 위함 
	public float jumpForce = 100f; 			//플레이어가 점프를 할때의 추가되는 힘의 양 

	public bool grounded = false;			// 플레이어가 땅에 있는지 아닌지 구별을 위한 변수
	public bool obstacle = false;			//플레이어가 벽돌에 점프했을때 닿았는지
	public bool groundedup = false;
	public bool inwater = false;			//물속인지 아닌지 체크
	public Transform waterCheck;			//물속인지 아닌지 체크

	public float moveForce = 365f;			// 플레이어의 왼쪽 오른쪽 이동을 위한 추가되는 힘의 양	
	public float maxSpeed = 5f;				// 가장 빠르게 x 축 안에서 플레이어가 이동 할수있는 최고 스피드

	public RectTransform stick;
	public float v; 						//점프 위치
	public bool jumponce;					

	public float sp;
	public Rigidbody2D gravityctrl;
	public Spawner1 swit;

	void Awake()
	{
		// 레퍼런스(참조)들을 셋팅.
		waterCheck = transform.Find ("waterCheck");
		anim = GetComponent<Animator>();
		//tikkiinwater = GetComponent<Transform>();
		//Tikki = GameObject.Find ("Tikki2").GetComponent<Transform> ();
		TK = GameObject.Find ("Tikki2").GetComponent<PlayerCtrl2> ();
		gravityctrl=GetComponent<Rigidbody2D> ();
		swit= GameObject.Find ("spawner1").GetComponent<Spawner1> ();
	}


	void Update()
	{
		
		// 플레이어 position 으로부터 groundcheck position 까지 linecast(두 점을 잇는 선을 그림 )할때 
		// 만약 충돌한 어떤객체가 Ground layer 라면 현재 플레이어는 땅에 있는거다.

		v = Input.GetAxis("Vertical");//수직 값 받음(-1~1)사이
		//v = UltimateJoystick.GetVerticalAxis ("BangBang"); //조이스틱 
		inwater= Physics2D.Linecast(waterCheck.position, transform.position,1 << LayerMask.NameToLayer("Water")); 

		if (inwater) {
			//밖으로 나왔을 때
			//if (TK.settransform) {
				//Tikki.position = tikkiinwater.position;
				//tikkiinwater.position = new Vector3 (-20f, -20f, 0f);
				//TK.followtkinwater.enabled = false;
				//TK.followtk.enabled = true;
				//swit.inthewater = false;
				//swit.Spawn ();
			//}
			//Destroy(gameObject);
			//TK.settransform = false;
		}

	
		// 만약 점프 버튼을 눌렀를때 플레이어가 땅에 있었다면 플레이어는 점프 할 수있다.
		if (Input.GetButtonDown("Vertical")&&jumponce==false||Input.GetButtonDown("Vertical")&& jumponce==false) 
		{
			jumponce = true;
			jump = true;
		}

		if(Time.time>sp)
		{
			jumponce = false;
			sp = Time.time +(15.0f * Time.deltaTime); //deltatime(프레임당 걸리는 시간)
		}

	}

	//고정 시간마다 호출 
	void FixedUpdate ()
	{

		//Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
		float h = Input.GetAxis("Horizontal");
		//h = UltimateJoystick.GetHorizontalAxis ("BangBang");

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

		// 만약 플레이어가 왼쪽을 바로볼때 플레이어를 오른쪽으로 이동하게 입력했다면 
		if(h > 0 && !dirRight)
			// 플레이어를 뒤집어라
			Flip();
		// 그렇지 않고 만약 플레이어가 오른쪽을 바로볼때 플레이어를 왼쪽으로 이동하게 입력했다면 
		else if(h < 0 && dirRight)
			// 플레이어를 뒤집어라
			Flip();

		Jump ();

	}


	void Jump()
	{		

		// 만약 플레이어가 점프를 한다면
		if(jump)
		{
			if (!jumponce)
				return;

			//Debug.Log ("jump");
			// animator의 trigger(전환) parameter에 Jump를 셋팅
			anim.SetTrigger("Jump");

			// jump sound가 플레이된다.
			GetComponent<AudioSource>().Play();

			//플레이어게게 수직 힘이 가해진다
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Update에서 조건이 만족하여 점프상태가 될때까지 확실하게 플레이어가 다시 점프를 못하게 만들어라 
			jump = false;
			v = 0f;
			grounded = false;
			//StartCoroutine ("Changebool");

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


}

