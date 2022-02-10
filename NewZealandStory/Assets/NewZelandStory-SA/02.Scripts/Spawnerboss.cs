using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rand=UnityEngine.Random; 

public class Spawnerboss : MonoBehaviour {


	public GameObject snow;//프리팹을 연결
	public Rigidbody2D rig;//프리팹의 Rigidbody2d
	private float random;//랜덤 생성 위함
	private float ransec;

	public Transform bo;
	public Transform ok;

	private float xforce;
	private float yforce;

	void Awake()
	{	
		random=Rand.Range(1,4);
		ransec=Rand.Range(0.1f,1.5f);
		InvokeRepeating ("StartSpawn", ransec, random);
		rig=snow.transform.root.GetComponent<Rigidbody2D>();

		bo = GameObject.Find ("Boss").GetComponent<Transform> ();
		ok = GetComponent<Transform> ();
		ok.position = bo.position;
    }
	void Update()
	{
		if (!GameObject.Find("TikkiBoss").GetComponent<PlayerCtrl3>().Change)
		{
			ok.position = new Vector3(bo.position.x + 0.8f, bo.position.y + 1f, bo.position.z);
		}
		else
			Destroy(gameObject);

	}


	void StartSpawn() 
	{
		xforce = Rand.Range (700,900);
		//yforce = Rand.Range (10,20);

		Rigidbody2D snowInstance = Instantiate (rig, transform.position, Quaternion.Euler (new Vector3 (1, 0, 0))) as Rigidbody2D;

		if(snow.GetComponent<Snow>().st==true)
			snowInstance.velocity = new Vector2 (0, 0);
		else
			snowInstance.velocity = new Vector2 (1, 1);//3D라면 Vector3로 받음. //velocity선택하면 한쪽으로 쭉 감

		snowInstance.AddForce (new Vector2 (xforce, 30));
		snowInstance.gravityScale = 0.2f;


		//Debug.Log ("spawner");
	}

}
