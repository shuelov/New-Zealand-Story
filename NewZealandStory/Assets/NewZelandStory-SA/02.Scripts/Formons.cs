using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formons : MonoBehaviour {

	public MonPig ene;
	private Animator mon1ai;
	private bool one;

	void Awake()
	{
		ene = gameObject.GetComponent<MonPig> ();
		mon1ai = GetComponent<Animator> ();
		StartAttack ();
	}
	// Use this for initialization
	void StartAttack () {
		StartCoroutine (Mon1AI ());
	}

	IEnumerator Mon1AI()
	{
		yield return new WaitForSeconds (1f);
		mon1ai.SetTrigger ("shoot");
		ene.moveSpeed = 0f;
		yield return new WaitForSeconds (1.5f);
		ene.moveSpeed = -1f;
		StartAttack ();
	}


}
