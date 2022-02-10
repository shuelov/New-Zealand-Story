using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : MonoBehaviour {
	public HiddenDoor hidden;

	// Use this for initialization
	void Awake () {
		hidden = GameObject.Find ("HiddenDoor").GetComponent<HiddenDoor> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D ccc) {
		if (hidden.open == true) 
		{
			ccc.isTrigger = false;
		}
	}


}
