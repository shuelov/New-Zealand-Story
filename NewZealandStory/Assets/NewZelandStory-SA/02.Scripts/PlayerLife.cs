using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
	private Animator anim;							// Player객체의 포함된 Animator 컴포넌트를 위한 Reference


	public float life = 10;							// 플레이어의 생명 변수 처음에 시작을 100으로 설정
	public float damageAmount = 10f;				// 적들이 플레이어를 타격할때 플레이어가 입는 데미지는의 양 
	public float hurtForce = 10f;					// 플레이어가 몬스터에게 타격당할때 설정 힘으로 밀려난다.
	public float repeatDamagePeriod = 2f;			// 얼마나 자주 플레이어가 데미지를 받을수 있는지를 설정

	private float lastHitTime;						// 플레이어가 마지막으로 타격 당했을때의 시간 

	public Bye BBBYYYEEE;

	public AudioClip Death;

	public AudioSource audios;
	private AudioSource backSnd;
	private Cage cage;

	private Transform deadposition;				   //죽은 자리 (아이템 떨굴 자리)
	[HideInInspector]
	public float lastpositionX;
	[HideInInspector]
	public float lastpositionY;

	public bool revive;								 //부활~
	public bool tikidead;
	public int Life = 2;
	public bool direct;
	public bool spikedead;
	private Transform groundCheck;

	void Awake ()
	{
		// 레퍼런스들의 셋팅 
		anim = GetComponent<Animator>();
		groundCheck = transform.Find("groundCheck");
		BBBYYYEEE=GameObject.Find("Bye").GetComponent<Bye>();
		audios = GetComponent<AudioSource>();
		backSnd = GameObject.Find("stage1map").GetComponent<AudioSource>();
		cage = GameObject.Find("cage").GetComponent<Cage>();
		deadposition = GetComponent<Transform>();
	}

	void Update() {
		if (!tikidead)
		{
			lastpositionX = deadposition.position.x;
			lastpositionY = deadposition.position.y;
		}
		spikedead = Physics2D.Linecast(groundCheck.position, transform.position,1 << LayerMask.NameToLayer("spike"));  //완전바닥
		if (spikedead)
			kill ();
	}

	// 충돌 CallBack 함수
	void OnCollisionEnter2D (Collision2D col)
	{
		// 만약 충돌한 gameobject가 몹이거나 몹 무기라면 
		if(col.gameObject.tag == "Enemy")
		{
			kill ();
		}

		else if(col.gameObject.tag=="MonsterWeapons")
		{
			kill ();
		}

		else if(col.gameObject.tag=="Spike")
		{
			kill ();
		}	

	}

	public void kill()
	{

        if (direct)
            StartCoroutine(byebye());

        else if (!cage.rescue && !revive)
        {
            tikidead = true;
            revive = true;

            //AudioSource의 사운드 연결
            audios.clip = Death;
            //사운드 플레이. Mute설정시 사운드 안나옴
            backSnd.enabled = false;

            audios.Play();

            anim.SetBool("Dead", true);
            StartCoroutine(byebye());

            if (Life == 0)
                // 사용자 PlayerCtrl script를 비 활성화 하자 
                GetComponent<PlayerCtrl>().enabled = false;
        }
    }

    IEnumerator byebye(){

		yield return new WaitForSeconds (0.5f);
		anim.SetBool("Dead", false);
		// gameobject에 포함된 collider들을 모두 찾은다음 그 컴포넌트들을 모두 trigger(충돌시 통과됨) 가 되도록 설정 
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}
		yield return new WaitForSeconds (1.5f);

		foreach (Collider2D c in cols)
		{
			c.isTrigger = false;
		}

		if (spikedead) {
			transform.position = new Vector3 (lastpositionX-1, lastpositionY, 0);
			spikedead = false;
		}
		transform.position =new Vector3(lastpositionX, lastpositionY, 0);


		//yield return new WaitForSeconds(1.5f);
        Life--;
        if (Life <= 0 || direct)
        {
            BBBYYYEEE.BYE();
            GameObject.Find("Timer").GetComponent<Timer>().enabled = false;
            audios.clip = Death;
            audios.Play();
            backSnd.enabled = false;            
            Destroy(gameObject);
        }
       else if (Life != 0&&!direct)
		{
			tikidead = false;
			revive = false;
			GameObject.Find ("Timer").GetComponent<Timer> ().Start ();
			backSnd.enabled = true;
		}


	}

}