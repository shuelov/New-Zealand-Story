using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroysnat : MonoBehaviour {


	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Wale") 
		{
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Player") 
		{
			col.transform.root.GetComponent<PlayerLifeBOSS>().kill();
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
