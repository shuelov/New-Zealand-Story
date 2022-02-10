using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{

	void Start () 
	{
		// 만약 아무데도 부딪히지 않는다면 1초뒤 Arrow를 파괴
		Destroy(gameObject,1f);
	}

	//void OnCollisionEnter2D (Collision2D col) 
	void OnTriggerEnter2D (Collider2D col) 
	{

		// 처음 stage에서 만약에 화살이 몬스터와 Hit 되었을때
		if (col.gameObject.tag == "Monster")
		{

			// Hit된 몬스터의 script를 찾고 Hurt()함수를 호출 
			col.gameObject.GetComponent<Enemy>().Hurt();
			// 화살 삭제
			Destroy(gameObject);

		}

		// 다음 스테이지에서 몬스터와 Hit 되었을때
		else if (col.gameObject.tag == "Mons" || col.gameObject.tag == "MonFrog" || col.gameObject.tag == "monAuch")
		{

			// Hit된 몬스터의 script를 찾고 Hurt()함수를 호출 
			col.gameObject.GetComponent<MonPig>().Hurt();
			// 화살 삭제
			Destroy(gameObject);

		}

		else if (col.gameObject.tag == "Ride")
		{
			if (col.transform.root.GetComponent<Riderat> ().Tikkion != true) {// Hit된 몬스터의 script를 찾고 Hurt()함수를 호출 
				col.gameObject.transform.root.GetComponent<MonPig> ().Hurt ();
				Destroy (col.gameObject, 2);
				// 화살 삭제
				Destroy (gameObject);
			}else
				// 화살 삭제
				Destroy(gameObject);

		}

		// 몬스터 무기와 Hit 되었을때
		else if (col.gameObject.tag == "MonsterThrows")
		{

			//몹이 던진 오브젝트 destroy
			Destroy(col.transform.root.gameObject);
			//화살삭제
			Destroy(gameObject);


		}

		// 보스(고래)와 Hit 되었을때
		else if (col.gameObject.tag == "Wale")
		{
			// Hit된 보스의 script를 찾고 bosshurt()함수를 호출 
			col.gameObject.transform.root.GetComponent<bosslife>().bosshurt();
			//화살삭제
			Destroy(gameObject);

		}

		else if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Spike")
		{
			//화살삭제
			Destroy(gameObject);
		}
		// 플레이어가 아닌 다른 것과 Hit 되었을때
		//else if (col.gameObject.tag != "Player")
		//화살삭제
		//	Destroy(gameObject);

	}

}
