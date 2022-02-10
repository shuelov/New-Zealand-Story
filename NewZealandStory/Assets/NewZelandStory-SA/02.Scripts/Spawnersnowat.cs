using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rand=UnityEngine.Random; 

public class Spawnersnowat : MonoBehaviour {


	public Rigidbody2D snowat;//프리팹을 연결
	private int random;//랜덤 생성 위함
	private float ransec;
	public Transform[] what;

	void Awake()
	{	
		random=Rand.Range(0,2);
		StartCoroutine (StartSpawn ());


    }
	void Update()
	{


	}
		
	public IEnumerator StartSpawn() 
	{
		//Debug.Log ("물이떨어진다");
		yield return new WaitForSeconds (1.5f);

		Rigidbody2D snowatInstance = Instantiate (snowat, what[random].position, Quaternion.Euler (new Vector3 (1, 0, 0))) as Rigidbody2D;
		snowatInstance.velocity = new Vector2 (0, 0);

		yield return new WaitForSeconds (0.5f);
		this.Awake ();

	}

}
