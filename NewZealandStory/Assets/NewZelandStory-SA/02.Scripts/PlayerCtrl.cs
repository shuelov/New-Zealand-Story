using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour {
	public GameObject uii;

	private Animator anim;					// Player 객체의 Animator component 를 위한 Reference(참조) 이다

	// 인스펙터에 노출 안됨 
	[HideInInspector]
	public bool dirRight = true;			// 플레이어의 현재 바라보는 방향을 알기 위함 


	[HideInInspector]
	public bool jump = false;	   			// 플레이어가 현재 점프중인지 아닌지 알기 위함 
	public float jumpForce = 100f;          //플레이어가 점프를 할때의 추가되는 힘의 양 
	public float v;                         //점프 위치
	public bool jumponce;
    public float h;


    public bool grounded = false;			// 플레이어가 땅에 있는지 아닌지 구별을 위한 변수
	public bool obstacle = false;			//플레이어가 벽돌에 점프했을때 닿았는지
	public bool groundedup = false;


	private Transform groundCheck;	 		//만약 플레이어가 땅에 있을때 position을 marking 할 곳
	public Transform obstacleCheck;

	public float moveForce = 365f;			// 플레이어의 왼쪽 오른쪽 이동을 위한 추가되는 힘의 양	
	public float maxSpeed = 5f;				// 가장 빠르게 x 축 안에서 플레이어가 이동 할수있는 최고 스피드

	public RectTransform stick;
	
	private Cage cage;
	private Animator cageanim;
	private Animator friendanim;
	public Collider2D Door;

	private Transform tikki;
	private Rigidbody2D rig;

	public HiddenDoor hidden;

	public BombThrow activebomb;

	public AudioClip Clear;
	public AudioSource audios;
	private AudioSource backSnd;
	public Transform hiddentrans;
    bool stagecleared = false;
    

	void Awake()
	{
		// 레퍼런스(참조)들을 셋팅.
		groundCheck = transform.Find("groundCheck");
		obstacleCheck = transform.Find ("obstacleCheck");
		anim = GetComponent<Animator>();
		cage= GameObject.Find("cage").GetComponent<Cage>();
		cageanim = GameObject.Find ("cage").GetComponent<Animator> ();
		friendanim = GameObject.Find ("friend").GetComponent<Animator> ();
		Door = GameObject.Find ("HiddenDoorBase").GetComponent<BoxCollider2D> ();
		tikki = GetComponent<Transform> ();
		rig = GetComponent<Rigidbody2D> ();
		hidden = GameObject.Find ("HiddenDoor").GetComponent<HiddenDoor> ();
		hiddentrans=GameObject.Find ("HiddenDoorBase").GetComponent<Transform> ();
		activebomb=GameObject.Find ("BombThrow").GetComponent<BombThrow> ();
		backSnd = GameObject.Find("stage1map").GetComponent<AudioSource>();
		audios = GetComponent<AudioSource>();
    }

	#if UNITY_STANDALONE_WIN

	void Start()
	{
		uii.SetActive (false);
	}

	void Update()
	{

	// 플레이어 position 으로부터 groundcheck position 까지 linecast(두 점을 잇는 선을 그림 )할때 
	// 만약 충돌한 어떤객체가 Ground layer 라면 현재 플레이어는 땅에 있는거다.
	grounded = Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Ground"));  //완전바닥
	groundedup= Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Obstacle")); //
	v = Input.GetAxis("Vertical");//수직 값 받음(-1~1)사이
	//v = UltimateJoystick.GetVerticalAxis ("BangBang"); //조이스틱 


	// 만약 점프 버튼을 눌렀를때 플레이어가 땅에 있었다면 플레이어는 점프 할 수있다.
	if (/*Input.GetButtonDown("Vertical") &&*/ grounded && v >= 0.5f && jumponce==false||/*Input.GetButtonDown("Vertical")&&*/groundedup&& v >= 0.5f &&  jumponce==false) 
	{
	jumponce = true;
	jump = true;
	}

	}

	//고정 시간마다 호출 
	void FixedUpdate ()
	{
        if (!stagecleared)
        {
      
            //Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
           h = Input.GetAxis("Horizontal");
            //Debug.Log(h);
            // animator 컴포넌트에 parameter(매개변수)인 Speed에 horizontal(수평) 입력값의 절대값(Mathf.Abs())을 셋팅한다.
            anim.SetFloat("Speed", Mathf.Abs(h));

            //만약 플레이어의 바라보는 방향이 바뀌거나 혹은 maxSpeed에 아직 도달하지 않을때 ( h(-1.0f~1.0f)는 velocity.x를 다르게 표시한다)
            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
                //플레이어 객체에 힘을 가한다.
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

            // 만약에 플레이어의 수평 속도가 maxSpeed 보다 크면 
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
                //플레이어의 velocity(속도)를 x축방향으로 maxSpeed 로 셋팅해줘라 또한 기존 rigidbody2D.velocity.y 도 셋팅 해 줘야 한다.
                // Mathf.Sign() 는 매개변수를 참조해서 1 또는 -1(float)을 반환  
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            // 만약 플레이어가 왼쪽을 바로볼때 플레이어를 오른쪽으로 이동하게 입력했다면 
            if (h > 0 && !dirRight)
                // 플레이어를 뒤집어라
                Flip();
            // 그렇지 않고 만약 플레이어가 오른쪽을 바로볼때 플레이어를 왼쪽으로 이동하게 입력했다면 
            else if (h < 0 && dirRight)
                // 플레이어를 뒤집어라
                Flip();


            Jump();

        }
	}

#endif

#if UNITY_ANDROID
	void Update()
	{
		
		// 플레이어 position 으로부터 groundcheck position 까지 linecast(두 점을 잇는 선을 그림 )할때 
		// 만약 충돌한 어떤객체가 Ground layer 라면 현재 플레이어는 땅에 있는거다.
		grounded = Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Ground"));  //완전바닥
		groundedup= Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Obstacle")); //

		v = UltimateJoystick.GetVerticalAxis ("BangBang"); //조이스틱 

	
		// 만약 점프 버튼을 눌렀를때 플레이어가 땅에 있었다면 플레이어는 점프 할 수있다.
		if (grounded && v >= 0.5f && jumponce==false||groundedup&& v >= 0.5f &&  jumponce==false) 
		{
			jumponce = true;
			jump = true;
		}

	}

	//고정 시간마다 호출 
	void FixedUpdate ()
	{
            if (!stagecleared)
        {
      
		//Input클래스안에 다음 GetAxis() 함수 호출로 horizontal 입력을 캐치한다.
		float h = UltimateJoystick.GetHorizontalAxis ("BangBang");

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
	}
#endif



    void Jump()
	{	
		obstacle=Physics2D.Linecast(transform.position, obstacleCheck.position, 1 << LayerMask.NameToLayer("Obstacle"));  

		// 만약 플레이어가 점프를 한다면
		if(jump)
		{
			if (!jumponce)
				return;

            rig.sharedMaterial.friction = 0;
            Collider2D[] cols = GetComponents<Collider2D> ();

			foreach (Collider2D c in cols) {
				if (c.gameObject.tag == "Wall") {
					if (c.tag == "Player")
						c.isTrigger = false;	
				}
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
			//StartCoroutine ("Changebool");
		}
	}

	IEnumerator Changebool()
	{
		Collider2D[] cols = GetComponents<Collider2D> ();

		foreach (Collider2D c in cols) {
			//Debug.Log ("점프중player trigger true");
			if (c.gameObject.tag != "Wall"&&c.gameObject.tag!="TOP") {
				if (c.tag == "Player")
					c.isTrigger = true;	
			}
		}

		//	jumponce = false;
		yield return new WaitForSeconds (0.15f);
		foreach (Collider2D c in cols) {
			//Debug.Log ("다시 player trigger false");
			if (c.tag == "Player")
				c.isTrigger = false;
                  rig.sharedMaterial.friction = 0.5f;
           }
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        Collider2D[] cols = GetComponents<Collider2D>();

        if (c.gameObject.tag == "Wall" || c.gameObject.tag == "TOP"||c.gameObject.tag=="NEVERCROSS")
        {
            foreach (Collider2D co in cols)
            {
                //Debug.Log ("점프하다가 벽이나 탑에 부딪힘");
                if (co.tag == "Player")
                {
                    co.isTrigger = false;
                }
            }
        }

        if (hidden.open == true) 
		{
			if (c.gameObject.tag == "HiddenDoorBase") {

				Door.isTrigger =false;
				tikki.position = new Vector3 (hiddentrans.position.x, hiddentrans.position.y, 0);
				//tikki.position = new Vector3 (9.06f, 3.18f, 0f);
				rig.simulated = false;
				StartCoroutine (Shortcut ());
			}
		}

    }

	void OnCollisionEnter2D (Collision2D col)
	{

		Collider2D[] cols = GetComponents<Collider2D> ();

		//만약 캐릭터와 바닥에 있을때 닿는 충돌체가 Ground이거나 Obstacle이면
		if (grounded||groundedup) {
            foreach (Collider2D c in cols)
            {
                //Debug.Log ("땅에서있어");
                if (c.tag == "Player")
                {
                    c.isTrigger = false;
                    rig.sharedMaterial.friction = 0.5f;
                    jumponce = false;
                }
            }
		}

        //만약 캐릭터와 ground아닐때 닿는 충돌체가 Ground이면
        if (col.gameObject.tag == "Ground"&&!grounded||col.gameObject.tag=="Obstacle"&&!grounded||col.gameObject.tag=="mon1turn"&&!grounded) 
		{
            foreach (Collider2D c in cols)
            {
                //Debug.Log ("땅에떨어짐");
                if (c.tag == "Player")
                {
                    c.isTrigger = false;
                   rig.sharedMaterial.friction = 0.5f;
                    jumponce = false;
                }
            }

		}
	
		if (col.gameObject.tag == "SetFree")
		{
            friendanim.SetTrigger("Free");
            cageanim.SetTrigger ("Free");
			
            anim.speed = 0.0f;
			cage.rescue = true;

			//AudioSource의 사운드 연결
			audios.clip = Clear;
            stagecleared = true;
            //사운드 플레이. Mute설정시 사운드 안나옴
            audios.Play ();
			backSnd.enabled = false;

			//스테이지1 클리어
			StartCoroutine(ChangeMap());

        }



		if (col.gameObject.tag == "Point")
		{
			col.transform.GetComponentInParent<Item> ().pluspoint ();
			//Debug.Log ("callpluspoint");
			if (col.transform.GetComponentInParent<Item> ().num == 5) 
			{
				GameObject.Find ("ArrowShoot").SetActive (false);
				activebomb.enabled = true;
			}
		}


	}

	IEnumerator Shortcut()
	{
		anim.SetTrigger("dissapear");
		GameObject.Find("HiddenDoorBase").GetComponent<Animator>().SetTrigger("open");
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("7.NS-finish");
		//SceneManager.LoadScene ("3.NS-Stage2");
	}

	IEnumerator ChangeMap()
	{

		yield return new WaitForSeconds(3.5f);
		SceneManager.LoadScene("5.NS-Boss");
        transform.root.gameObject.SetActive(false);
        //SceneManager.LoadScene("4.NS-StageBoss");
    }


	// 캐릭터의 현재 방향을 바꿔주는 함수 
	void Flip ()
	{
		//플레이어의 바라보는 방향을 바꾸자 
		dirRight = !dirRight;
		//Debug.Log ("flip할시간");
		//플레이어의 local scale x에 -1을 곱하자
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}
