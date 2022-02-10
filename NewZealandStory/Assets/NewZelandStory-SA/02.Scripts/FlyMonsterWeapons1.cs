using UnityEngine;
using System.Collections;

public class FlyMonsterWeapons : MonoBehaviour 
{
	public float moveSpeeds=-20f;//몬스터무기의 이동속도 '-'면 왼쪽으로 이동
	public Rigidbody2D weapon;

	private float HP=1f;
	public MonPig enemies;//Enemy script를 위한 Reference
	//private Animator anim;
	//private PlayerLife killplayer;
	private SpriteRenderer ren;//SpriteRenderer 컴포넌트를 위한 레퍼런스
	private float sp;

	void Awake()
	{
		//레퍼런스들의 셋팅
	
		enemies=transform.root.GetComponent<MonPig>();
		//killplayer = GameObject.Find ("Tikki2").GetComponent<PlayerLife> ();
		//weapon = transform.root.GetComponent<Rigidbody2D> ();  
		//rigidbody2D = GameObject.Find("mon1_throw").GetComponent<Rigidbody2D> ();
		//anim = GetComponent<Animator> ();
	}

	void FixedUpdate()
	{

		//만약 몬스터의 HP가 0또는 0미만이고 아직 살아있다면 죽이자..
		if (HP <= 0 && enemies.dead)
			//Death()함수 호출
			Death ();
	}

	void Update()
	{
		
		//if (enemies.moveSpeed == 0) 
		//{
			//new WaitForSeconds (1);
			//moveSpeeds = 20f;

			//GetComponent<AudioSource> ().Play ();
			if(Time.time>sp)
			{
			//만약 몬스터가가 오른쪽 방향이라면
				if (enemies.dirRight) {
				//오른쪽 방향으로 무기 생성하고 오른쪽으로 무기의 속도를 셋팅
					Rigidbody2D bulletInstance = Instantiate (weapon, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (moveSpeeds, 0);//3D라면 Vector3로 받음. //velocity선택하면 한쪽으로 쭉 감

					//Debug.Log ("dd");
					//Instantiate로 생성하면 return값이 object라 형변환필요 
					//만약 Rigidbody2D로 받지않고 gameObject=ga로 받았다면 ga.GetComponent<Rigidbody2D>().velocity로 접근해야해서'.'더 쓰게됨 비추.
					//public를 사용해서 드래그드롭하면 제일 짧지만 Insepctor에 노출되는 public변수 선언은 아껴줘야함. 
				} else {
				//그렇지 않으면 왼쪽 방향으로 무기 생성하고 왼쪽으로 무기의 속도를 셋팅
					Rigidbody2D bulletInstance = Instantiate (weapon, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (-moveSpeeds, 0);
					//Debug.Log ("aa");
				}
				sp = Time.time +(250.0f * Time.deltaTime); //deltatime(프레임당 걸리는 시간)
			}
		//}
		//if(enemies.dirRight)
		//	bulletInstance.velocity = new Vector2 (-moveSpeeds, 0);
		//else if(!enemies.dirRight)
		//	bulletInstances.velocity = new Vector2 (moveSpeeds, 0);
	}

	void Death()
	{
		Destroy (gameObject);
	}

}

