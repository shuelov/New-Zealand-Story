using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Rand=UnityEngine.Random; //Rand.range로 랜덤 함수 쓸 수 있게
public class MonPig : MonoBehaviour {
	public enum MODE_KIND { Pig = 1,Bird, Auch, Frog, Mon1, Mon2 };//몬스터마다 속도나 다른 설정다르게 할 수 있음
	[Header("SETTING")]
	public MODE_KIND enemyKind = MODE_KIND.Pig;

	private Animator anim;					// Player 객체의 Animator component 를 위한 Reference(참조) 이다
	public GameObject Dupi;

	// 인스펙터에 노출 안됨 
	[HideInInspector]
	public bool dirRight = false;			// 몬스터 현재 바라보는 방향을 알기 위함 

	public bool grounded = false;			// 몬스터가 땅에 있는지 아닌지 구별을 위한 변수
	public bool front = false;				// 몬스터가 벽돌에 점프했을때 닿았는지

	public bool dead = false;				//몬스터가 죽었는지 아닌지를 위한 변수

	public Rigidbody2D pigarrow;			//몬스터 무기 프리팹

	private Transform groundCheck;	 		//만약 몬스터가 땅에 있을때 position을 marking 할 곳
	private Transform frontCheck;
	public Transform monthrow;
	public Transform momauchspawn;

	public float moveForce = 100f;			// 몬스터의 왼쪽 오른쪽 이동을 위한 추가되는 힘의 양	
		
	public float moveSpeed=-0.5f;			//몬스터의 이동속도 '-'면 왼쪽으로 이동
	public int HP=1;						//몬스터의 생명력


	private Transform deadposition;			//죽은 자리 (아이템 떨굴 자리)
	[HideInInspector]
	public float lastpositionX;
	[HideInInspector]
	public float lastpositionY;

	public DropNextStage DoDrop;			 //아이템 떨구는 스크립트 연결

	public Riderat ride;                     //탈 것 스크립트 연결(생쥐)
	public Rideduck ride1;                   //탈 것 스크립트 연결(오리)


	public Transform tipo;					//플레이어 위치
	public bool attacktrue;					//공격중인지

	public float tracedistance = 3f;		//인식 및 공격 거리

	public bool check;						//Ray 센서를 위한 변수

	public Rigidbody2D rbody2D;

	private int num;
	private float timewait;

	public bool flip;		

	public enum MODE{Idle=1, Attack,Move};
	public MODE EnemyMode = MODE.Idle;

	public bool follow;
	public bool move;

	public float dist;
	public Vector3 targetdir;

	private float h=0;
	private bool justthrow;

	private float ticktack;
	private float ticktock;

	public Transform Ridddde;
	public Riderat ew;

	private bool oneRespawn;

	[HideInInspector]
	public GameObject[] Auches;
	[HideInInspector]
	public int CountAuches;

	void Awake()
	{
		// 레퍼런스(참조)들을 셋팅.
		groundCheck = transform.Find("groundCheck");
		frontCheck = transform.Find ("frontCheck");
		monthrow = transform.Find ("monThrow");
		momauchspawn = transform.Find ("monauchSpawn");
		anim = GetComponent<Animator>();
		rbody2D = GetComponent<Rigidbody2D> ();
		deadposition = GetComponent<Transform> ();
		DoDrop = GetComponentInChildren<DropNextStage> ();
		tipo=GameObject.Find ("Tikki2").GetComponent<Transform> ();
	}

	void Start()
	{

			//랜덤으로 행동 실행
			StartCoroutine(PickOneMotion());
	}


	IEnumerator PickOneMotion()
	{
		if (!follow)
		{
			num = Rand.Range(1, 3);
			timewait = Rand.Range(0f, 6f);
			yield return new WaitForSeconds(timewait);

			if (num == 1)
				EnemyMode = MODE.Idle;
			else if (num == 2)
				EnemyMode = MODE.Attack;
			else if (num == 3)
			{
				EnemyMode = MODE.Move;
			}
			StartCoroutine(SetMode());
		}
		else
			yield break;
	}

	IEnumerator SetMode()
	{
		while (!dead)
		{
			switch (EnemyMode)
			{
				case MODE.Idle:
					moveSpeed = 0;
					moveForce = 0;
					break;
				case MODE.Attack:	
					if (enemyKind == MODE_KIND.Auch)
					{
						break;
					}
					else
						justthrow = true;
					break;
				case MODE.Move:
					move = true;
					break;
			}
			yield return null;
		}
	}


	//고정 시간마다 호출 
	void FixedUpdate()
	{

		//그냥 산책중일때
		if (move)
		{
			// 추적중 아님
			if (!follow)
			{

				if (dirRight == false)
				{
					if (follow)
						// 도중에라도 추적중되면 함수 빠져나가기
						return;

					else
					{
						moveSpeed = -0.5f;
						moveForce = 70f;//움직이는 힘 테스트 끝나면 줄일것. 너무 빠름
						h = 0.5f;

						GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
						// animator 컴포넌트에 parameter(매개변수)인 Speed에 horizontal(수평) 입력값의 절대값(Mathf.Abs())을 셋팅한다.
						anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
						//몬스터의 속도를 x축방향 moveSpeed으로 셋팅
					rbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rbody2D.velocity.y);//대문자시작:컴포넌트. 소문자시작:참조변수
				
					}
				}
				else if (dirRight == true)
				{
					if (follow)
						// 도중에라도 추적중되면 함수 빠져나가기
						return;

					else
					{

						moveSpeed = -0.5f;
						moveForce = 70f;
						h = 0.5f * -1;

						GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
						anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
						rbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rbody2D.velocity.y);//대문자시작:컴포넌트. 소문자시작:참조변수
				
					}
				}
			}
		}



		//만약 몬스터의 HP가 0또는 0미만이고 아직 살아있다면 죽이자..
		if (HP <= 0 && !dead)
			//Death()함수 호출
			Death();

	}


	void Update () 
	{
		Auches = GameObject.FindGameObjectsWithTag("monAuch");
		CountAuches = Auches.Length;

		if (!dead)
		{
			//나중에 아이템 떨굴 때 위치 알기위함
			lastpositionX = deadposition.position.x;
			lastpositionY = deadposition.position.y;
		}

		// 플레이어 position 으로부터 groundcheck position 까지 linecast(두 점을 잇는 선을 그림 )할때 
		// 만약 충돌한 어떤객체가 Ground layer 라면 현재 플레이어는 땅에 있는거.
		grounded = Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("Ground"));  // 완전 바닥
		front= Physics2D.Linecast(frontCheck.position, transform.position,1 << LayerMask.NameToLayer("Wall")); // 벽에 부딪혔는지 확인

		if (front) {
			// 벽에 충돌하면 방향전환
			flip = true;
			Flip ();
		}

		// 몹과 플레이어 사이의 거리
		dist = Vector3.Distance(tipo.position, transform.position);

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// 1. 만약 인식거리안에 플레이어 들어오면
		if (tracedistance > dist) {
			move = false;

			//따라가기 bool
			follow = true;

			//추적 함수 실행
			TracePlayer ();

			// 1) 가까이에 플레이어가 있을때 
			if (dist < 3) {
				moveSpeed = 0f;	
				moveForce = 0f;
				move = false;
				follow = false;
				attacktrue = true;


				// a. 공격모드+가까이에 플레이어 있을때
				if (attacktrue && check||justthrow&&check) 
				{
					if (enemyKind == MODE_KIND.Frog)
						StartCoroutine(PickOneMotion());
					else if (enemyKind == MODE_KIND.Auch)
						return;
					else
					{

						if (Time.time > ticktock)
						{
							anim.SetTrigger("shoot");

							if (dirRight)
							{
			
								Rigidbody2D Throwpigarrow = Instantiate(pigarrow, monthrow.position, Quaternion.Euler(targetdir)) as Rigidbody2D;
								Throwpigarrow.velocity = new Vector2(-20, 0);
							}
							else if (!dirRight)
							{
		
								Rigidbody2D Throwpigarrow = Instantiate(pigarrow, monthrow.position, Quaternion.Euler(targetdir)) as Rigidbody2D;
								Throwpigarrow.velocity = new Vector2(20, 0);
							}
							ticktock = Time.time + (80.0f * Time.deltaTime);
						}

					}
				}
					attacktrue = false;
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// 2. 공격범위 안에 플레이어 없고 일반 공격 모드 일 때
		else 
		{
			move = false;
			follow = false;
			attacktrue = false;
			check = false;
		
			if (justthrow && !check) {
				if (enemyKind == MODE_KIND.Frog)
					StartCoroutine(PickOneMotion());
				else if (enemyKind == MODE_KIND.Auch)
					return;
				else {
					if (Time.time > ticktock)
					{
						anim.SetTrigger ("shoot");

						if (dirRight) {		
			
							Rigidbody2D Throwpigarrow = Instantiate (pigarrow, monthrow.position, Quaternion.Euler (targetdir)) as Rigidbody2D;
							Throwpigarrow.velocity = new Vector2 (-20, 0);
						} else if (!dirRight) {
	
							Rigidbody2D Throwpigarrow = Instantiate (pigarrow, monthrow.position, Quaternion.Euler (targetdir)) as Rigidbody2D;
							Throwpigarrow.velocity = new Vector2 (20, 0);
						}
						ticktock = Time.time + (80.0f * Time.deltaTime);
					}
				}
			}
			justthrow = false;
			move = true;
			StartCoroutine(PickOneMotion());
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	}
	

	public void Hurt()
	{
		//몬스터 생명력을 1만큼 줄인다
		HP--;
	}

	//추적로직
	public void TracePlayer()
	{

		if(follow==true)
		{
			targetdir = tipo.position - transform.position;
			targetdir.Normalize ();
			GetComponent<Rigidbody2D> ().AddForce (targetdir * -0.5f * moveForce);
			transform.position += targetdir * Time.deltaTime;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (enemyKind == MODE_KIND.Frog||enemyKind==MODE_KIND.Auch) {
			if (col.gameObject.tag == "Player") {
				PlayerLife2 TKlife = GameObject.Find ("Tikki2").GetComponent<PlayerLife2> ();
				TKlife.kill ();
			}
		}


		if (enemyKind == MODE_KIND.Auch)
		{
			if (col.gameObject.tag == "monAuch")
			{
				flip = true;
				Flip();
			}
		}

		if (enemyKind == MODE_KIND.Pig) {
			if (col.gameObject.tag == "Player") {
				Death ();
			}
		}

		if (col.gameObject.tag == "Wall"||col.gameObject.tag=="Spike") {
			Flip ();
		}

		if (col.gameObject.tag == "Player") {
			Death ();
		}

	}

	void OnTriggerEnter2D(Collider2D c)
	{
		// 추적하기위함
		if (c.gameObject.tag == "Player") {
			check = true;
		}

		if (c.gameObject.tag == "Flip") {
			flip = true;
		}

	}

	void OnTriggerExit2D(Collider2D co)
	{		
		// 추적 포기
		if (co.gameObject.tag == "Player") 
		{
			check = false;
		}
	}



	
	// 캐릭터의 현재 방향을 바꿔주는 함수 
	public void Flip ()
	{
		if (!flip)
			return;

		else 
		{		
			//플레이어의 바라보는 방향을 바꾸자 
			if (dirRight==true)
				dirRight = false;
			else
				dirRight = true;
			
			//플레이어의 local scale x에 -1을 곱하자
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			flip = false;
		}

	}

	public void Death()
	{
		if (enemyKind == MODE_KIND.Pig)
		{
			//ride = GameObject.Find("riderat").GetComponent<Riderat>();
			ride = GetComponentInChildren<Riderat>();
			Ridddde = GameObject.Find("riderat").GetComponent<Transform>();
			ride.GetOFF();
			Ridddde.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		}
		else if (enemyKind == MODE_KIND.Bird)
		{
			ride1 = GameObject.Find("rideduck").GetComponent<Rideduck>();
			Ridddde = GameObject.Find("rideduck").GetComponent<Transform>();
			ride1.GetOFF();
			Ridddde.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		}

		moveSpeed = 0f;
		moveForce = 0f;
		anim.SetFloat ("Speed", 0f);
		anim.SetTrigger("dead");

		//dead를 true로셋팅
		dead = true;

		//아이템 떨구기
		DoDrop.drop = false;
		DoDrop.Update();

		StartCoroutine(Destroymon());
	}

	//몬스터삭제
	IEnumerator Destroymon()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}

}



