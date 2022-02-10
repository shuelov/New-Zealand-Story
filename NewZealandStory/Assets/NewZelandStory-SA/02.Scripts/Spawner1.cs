using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour {
	//public float spawnTime = 5f;//각 spawn사이에 시간의 양(딜레이타임)
	//public float spawnDelay =3f;//spawn을 시작하기 전에 시간의양(딜레이타임)
	public GameObject[] tikki;//enemy프리팹들을 저장할 수 있는 배열
	public bool inthewater;


	// Use this for initialization
	void Start () {

	}

	public void Spawn()
	{
		if (inthewater) {
			Debug.Log ("12");
			Instantiate (tikki [1], transform.position, transform.rotation);
		}
		else
			Instantiate (tikki[0], transform.position, transform.rotation);
	}


	void Update () {
		
	}
}
