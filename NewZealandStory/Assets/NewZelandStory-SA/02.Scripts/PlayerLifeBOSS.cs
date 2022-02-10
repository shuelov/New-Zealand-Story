using UnityEngine;
using System.Collections;

public class PlayerLifeBOSS : MonoBehaviour {

	private Animator anim;						// Player객체의 포함된 Animator 컴포넌트를 위한 Reference

	private PlayerCtrl3 playerCtrl;				// PlayerCtrl 컴포넌트를 위한 Reference

	public AudioClip[] DeadClips;				// 플레이어가 데미지를 받을때 플레이되는 클립 배열 

	public float life = 10;					// 플레이어의 생명 변수 처음에 시작을 100으로 설정
	public float damageAmount = 10f;			// 적들이 플레이어를 타격할때 플레이어가 입는 데미지는의 양 
	public float hurtForce = 10f;				// 플레이어가 몬스터에게 타격당할때 설정 힘으로 밀려난다.
	public float repeatDamagePeriod = 2f;		// 얼마나 자주 플레이어가 데미지를 받을수 있는지를 설정

	private float lastHitTime;					// 플레이어가 마지막으로 타격 당했을때의 시간 

	public Bye BBBYYYEEE;

	public AudioClip Death;

	public AudioSource audios;
	private AudioSource backSnd;

	void Awake ()
	{
		// 레퍼런스들의 셋팅 
		anim = GetComponent<Animator>();
		playerCtrl = GetComponent<PlayerCtrl3>();
		BBBYYYEEE=GameObject.Find("Bye").GetComponent<Bye>();
		audios = GetComponent<AudioSource>();
		backSnd = GameObject.Find("BOSSMAP").GetComponent<AudioSource>();
	}



	// 충돌 CallBack 함수
	void OnCollisionEnter2D (Collision2D col)
	{
		// 만약 충돌한 gameobject가 몹이거나 몹 무기라면 
		if(col.gameObject.tag == "Enemy")
		{
			// 그리고 만약 시간이 (마지막 충돌된 시간 + 재 충돌의 시간) 를 초과 한다면 
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// 그리고 만약 플레이어가 아직까지 생명력이 0 이상이면 
				if(life > 0f)
				{
					// 데미지를 줘라 그리고 lastHitTime을 리셋하자
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}

				if(life<=0f)
				{
					kill ();


				}
			}
		}

		else if(col.gameObject.tag=="MonsterWeapons")
		{
			// 그리고 만약 시간이 (마지막 충돌된 시간 + 재 충돌의 시간) 를 초과 한다면 
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// 그리고 만약 플레이어가 아직까지 생명력이 0 이상이면 
				if(life > 0f)
				{
					// 데미지를 줘라 그리고 lastHitTime을 리셋하자
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}

				if(life<=0f)
				{
					kill ();

				}
			}
		}


		else if(col.gameObject.tag=="Spike")
		{
			// 그리고 만약 시간이 (마지막 충돌된 시간 + 재 충돌의 시간) 를 초과 한다면 
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// 그리고 만약 플레이어가 아직까지 생명력이 0 이상이면 
				if(life > 0f)
				{
					// 데미지를 줘라 그리고 lastHitTime을 리셋하자
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}

				if(life<=0f)
				{
					kill ();

				}
			}
		}	

	}

	public void kill()
	{
		if (!playerCtrl.fin) {
			// 플레이어의 모든 sprite들을 맨 앞으로 이동 시키자.
			SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer s in spr) {
				s.sortingLayerName = "UI";
			}

			//AudioSource의 사운드 연결
			audios.clip = Death;
			//사운드 플레이. Mute설정시 사운드 안나옴

			backSnd.enabled = false;

			audios.Play ();

			//Debug.Log ("졸려");
			anim.SetBool ("Dead", true);
			//anim.SetTrigger("dead");

			StartCoroutine (byebye ());

			// 사용자 PlayerCtrl script를 비 활성화 하자 
			GetComponent<PlayerCtrl3> ().enabled = false;
		}
	}

	IEnumerator byebye(){

		yield return new WaitForSeconds (0.5f);

		// gameobject에 포함된 collider들을 모두 찾은다음 그 컴포넌트들을 모두 trigger(충돌시 통과됨) 가 되도록 설정 
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}
		yield return new WaitForSeconds (1.5f);
		BBBYYYEEE.BYE ();
		Destroy (gameObject);

	}

		// 적에게 데미지를 받는 함수
	public void TakeDamage (Transform enemy)
	{
		//반드시 플레이어가 점프를 할수없게 하자 
		playerCtrl.jump = false;

		// 위쪽 힘과 함께 적에서 부터 플레이어까지를 담을 수 있는 vector의 생성 
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// hurtVector 와 hurtForce(10으로 설정해놓음)에 의해서 곱해진 Vector의 방향으로 플레이어에게 힘을 가하자 
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		//플레이어의 life를 10 줄이자
		life -= damageAmount;

	}

}