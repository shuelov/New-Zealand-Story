using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour {

	public bool rescue=false;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (rescue) {
			Destroy (gameObject);
			//Debug.Log ("해방");
		}
	}
		


}
