using UnityEngine;
using System.Collections;

public class ArrowPig : MonoBehaviour 
{
	public MonPig mon;
	public Vector3 dd;
	public bool flip;

	void Start () 
	{
		// 만약 그전에 파괴되지 않는다면 2초뒤 Arrow를 파괴
		Destroy(gameObject, 1);
		//mon=GameObject.Find("monpig").GetComponent<MonPig>();
	/*
		if (!mon.dirRight) {
			dd = transform.localScale;
			dd.x *=-1f;
		}
		*/
	}

	void Update()
	{


	}

	void OnCollisionEnter2D (Collision2D col) 
	{

		// 만약에 화살이 몬스터와 Hit 되었을때...
		if (col.gameObject.tag == "Player") {
			// Hit된 몬스터의 Enemy script를 찾고 Hurt()함수를 호출 
			col.gameObject.GetComponent<PlayerLife2> ().kill ();
			// 화살 삭제
			Destroy (gameObject);

			//그렇지 않고 만약에 화살이 몹 무기와 충돌했을때
		} else if (col.gameObject.tag == "Arrow") {

			//몹이 던진 오브젝트 destroy
			Destroy (col.transform.root.gameObject);
			//화살삭제
			Destroy (gameObject);

		} /*else if (col.gameObject.tag != "Monster"||col.gameObject.tag!="Mons"||col.gameObject.tag!="MonFrog")
			Destroy (gameObject);*/

		// 그렇지 않고 만약에 충돌한 게임오브젝트의 Tag가 Player가 아니라면...(즉 로켓이 Player 객체를 뺀 나머지 오브젝트와 충돌할 경우)
		//else if(col.gameObject.tag != "Player")
		//{
		//	// 폭발 이펙트 객체를 생성하고 화살 삭제 
		//	Destroy (gameObject);
		//	Debug.Log ("지니");
		//}
	}


}
