using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject enemie;//enemy프리팹 저장
	//몬스터 종류
	public enum MODE_KIND{ENEMY1=1,ENEMY2,ENEMY3};//몬스터마다 속도나 다른 설정다르게 할 수 있음
	[Header("SETTING")]
	public MODE_KIND enemyKind = MODE_KIND.ENEMY1;
	int num;

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawn());
	}

	IEnumerator Spawn()
	{
		switch (enemyKind) {
		case MODE_KIND.ENEMY1:
			num = Random.Range (1, 2);
			yield return new WaitForSeconds (num);
			GameObject enemy1 = Instantiate (enemie, transform.position, transform.rotation);
                enemy1.transform.parent = transform.root.GetComponent<Transform>();
			yield break;

		case MODE_KIND.ENEMY2:
			num = Random.Range (1, 3);
			yield return new WaitForSeconds (num);
                GameObject enemy2 = Instantiate(enemie, transform.position, transform.rotation);
                enemy2.transform.parent = transform.root.GetComponent<Transform>();
                yield break;

		case MODE_KIND.ENEMY3:
			num = Random.Range (3, 5);
			yield return new WaitForSeconds (num);
                GameObject enemy3 = Instantiate(enemie, transform.position, transform.rotation);
                enemy3.transform.parent = transform.root.GetComponent<Transform>();
                yield break;
		}

	}

}
