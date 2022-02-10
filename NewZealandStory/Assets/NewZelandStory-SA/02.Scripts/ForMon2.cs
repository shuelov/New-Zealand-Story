using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMon2 : MonoBehaviour {
	
	public Enemy ene;
	private Animator mon2ai;

	void Awake()
	{
		ene = gameObject.GetComponent<Enemy> ();
		mon2ai = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (Mon1AI ());
	}

	IEnumerator Mon1AI()
	{
		yield return new WaitForSeconds (3f);
		mon2ai.SetTrigger ("monattack");
		ene.moveSpeed = 0f;
		yield return new WaitForSeconds (1.5f);
		ene.moveSpeed = -1f;
		Start ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}


}
