using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bosslife : MonoBehaviour {
	public float bosshp=10;
	public GameObject explode;
	public bool DeathWale;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void bosshurt(){
		bosshp--;
		if (bosshp <= 0)
			death ();
	}

	public void death(){
		DeathWale = true;
		explode.SetActive (true);
		GameObject.Find ("TikkiBoss").GetComponent<Rigidbody2D> ().gravityScale = 0;
		GameObject.Find ("TikkiBoss").GetComponent<PlayerCtrl3> ().ChangeScene();
		Destroy (gameObject);
	}


}
