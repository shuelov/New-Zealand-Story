using UnityEngine;
using System.Collections;

public class Boomya : MonoBehaviour 
{

	public Sprite bombexplode;					//폭탄터지는 스프라이트

	private Animator anim;						//animator 컴포넌트를 위한 레퍼런스

	private SpriteRenderer render;				//SpriteRenderer 컴포넌트를 위한 레퍼런스

	public PlayerCtrl tikkidirection;				//플레이어가 바라보는 방향

	public BombThrow gam;						//폭탄 발사되는 스크립트

	void Awake()
	{
		anim = GetComponent<Animator> ();
		tikkidirection = GameObject.Find ("Tikki1").GetComponent<PlayerCtrl> ();
		gam = GameObject.Find ("BombThrow").GetComponent<BombThrow>();
		gam.weaponSpeed = 10f;
	}

	void Start () 
	{
		// 폭탄 삭제할 코루틴 함수 호출
		StartCoroutine (Destroybomb ());
	}

	// 아무것도 부딪히지 않았을 때 1초 후 자동 삭제
	IEnumerator Destroybomb()
	{
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{

		// 폭탄이 몬스터와 Hit 되었을때...
		if (col.tag == "Monster")
		{
			 //폭탄 터지는 애니메이션 작동
			anim.SetTrigger ("Explode");
			// Hit된 몬스터의 Enemy script를 찾고 Hurt()함수를 호출
			col.gameObject.GetComponent<Enemy> ().Hurt ();
			// 폭탄 삭제를 위한 코루틴 시작
			StartCoroutine ("Boomgone");
		}

		// 폭탄이 비밀의 문을 Hit 했을 때
		if (col.tag=="HiddenDoor")
		{
			// 폭탄 터지는 애니메이션 작동
			anim.SetTrigger("Explode");
			// Hit된 비밀 문의 Hiddendoor script를 찾고 DoorDamaged()함수를 호출 
			col.gameObject.GetComponent<HiddenDoor> ().DoorDamaged ();
			// 폭탄 삭제를 위한 코루틴 시작
			StartCoroutine("Boomgone");
		}

		// 폭탄이 몹의 무기와 충돌했을때
		else if (col.tag == "MonsterThrows")
		{
			// 폭탄 터지는 애니메이션 작동
			anim.SetTrigger("Explode");
			// 몹이 던진 오브젝트 destroy
			Destroy (col.transform.root.gameObject);
			// 폭탄 삭제를 위한 코루틴 시작
			StartCoroutine("Boomgone");
		}

		// 그렇지 않고 만약에 충돌한 게임오브젝트의 Tag가 Player가 아니라면
		else if(col.gameObject.tag != "Player")
		{
			// 폭탄 터지는 애니메이션 작동
			anim.SetTrigger ("Explode");
			// 폭탄 삭제를 위한 코루틴 시작
			StartCoroutine("Boomgone");
		}
	}

	IEnumerator Boomgone()
	{	
		// 폭탄 제자리에 멈추기
		gam.weaponSpeed = 0F;
		// 0.3초후 폭탄 삭제
		yield return new WaitForSeconds (0.3f);
		Destroy (gameObject);
	}

}
