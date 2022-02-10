using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour {
	//public float spawnTime = 5f;//각 spawn사이에 시간의 양(딜레이타임)
	//public float spawnDelay =3f;//spawn을 시작하기 전에 시간의양(딜레이타임)
	public GameObject[] enemies;//enemy프리팹들을 저장할 수 있는 배열

    void Awake()
    {
        StartCoroutine("StartSpawn");
    }

	// Use this for initialization
	IEnumerator StartSpawn() {
		//다음 함수의 호출로 일정 딜레이 이후 주기적으로 Spawn함수의 호출을 시작한다
		//InvokeRepeating ("Spawn", spawnDelay, spawnTime);	
		Instantiate (enemies[0], transform.position, transform.rotation);

        yield return new WaitForSeconds(0.3f);
        Instantiate(enemies[1], transform.position, transform.rotation);

    }

	void Spawn()                                                                                                
	{//랜덤 enemy객체를 생성(인스턴스화)
		//int enemyindex = Random.Range (0, enemies.Length);
		//Instantiate (enemies [0], transform.position, transform.rotation);
		//해당 객체를 포함하여 하위로 모든 Particle System컴포넌트를 찾아와 spawning effect를 플레이
		//foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>()) {
		//	p.Play ();
		//}
	}


	//참고)Enemy Respawn 시간을 지연하는 예제
	//InvokeRepeating("Spawn",1f,Random.RangeInt(minRSinterval,respawnInterval));
	//첫번째 인자는 함수이름이고 두번째는 함수가 실행 될 지연시간이다. 마지막 인자는 함수가 첫 실행 된 후 일정시간 간격으로 반복호출하는 시간을 넘겨주면 된다. 하지만 위의 함수를 실행하면 제대로 동작하지않는다. InvokeRepeating의 경우 당연히 처음으로 넘긴 파라미터 값의 시간이 적용되지 때문이다. 이것이 StartCoroutine과 InvokeRepeating함수를 구분해서 사용해야하는 이유이다.
	//InvokeRepeating("Spawn",1f,IntervalTime);
	//또한 StartCoroutine이 StopCoroutine이 있듯이 Invoke함수도 CancelInvoke()함수가 있다. 그리고 IsInvoking()함수도제공한다

	// Update is called once per frame
	void Update () {
		
	}
}
