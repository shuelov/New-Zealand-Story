using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMon1 : MonoBehaviour {
	
	public Enemy ene;
	private Animator mon1ai;
	int justtwo = 0;
	private bool one;

	void Awake()
	{
		ene = gameObject.GetComponent<Enemy> ();
		mon1ai = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (Mon1AI ());
	}

	IEnumerator Mon1AI()
	{
		yield return new WaitForSeconds (2f);
		mon1ai.SetTrigger ("monattack");
		ene.moveSpeed = 0f;
		yield return new WaitForSeconds (1.5f);
		ene.moveSpeed = -0.5f;
		Start ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}


	void OnTriggerEnter2D(Collider2D cl)
	{
		
		if (!one) {
			if (justtwo < 6) {
			
				if (cl.gameObject.tag == "mon1turn") {
					ene.Flip ();
					//Debug.Log ("산만해");
					justtwo++;
				} 
			} else if (justtwo == 6)
				Debug.Log ("이제떨어져");
			cl.isTrigger = false;
			one = true;
		}
	}

}
