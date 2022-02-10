using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMon3 : MonoBehaviour {
	
	public Enemy ene;
	private Animator mon3ai;
	int justtwo = 0;
	public float maxdistance=7f;
	public float rotationspeed=5f;
	public float movespeeds=-0.8f;

	public Enemy enemy;

	void Awake()
	{
		ene = gameObject.GetComponent<Enemy> ();
		mon3ai = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (Mon1AI ());
	}

	IEnumerator Mon1AI()
	{
		yield return new WaitForSeconds (2f);
		mon3ai.SetTrigger ("monattack");
		ene.moveSpeed = 0f;
		yield return new WaitForSeconds (1.5f);
		ene.moveSpeed = -0.8f;
		Start ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}


	void OnTriggerEnter2D(Collider2D cl)
	{
		
		if (justtwo < 4) {
			
			if (cl.gameObject.tag == "mon1turn") {
				ene.Flip ();
//Debug.Log ("산만해");
				justtwo++;
			} 
		}

		else if(justtwo==4)
		//	Debug.Log ("이제떨어져");
			cl.isTrigger = false;

	}

}
