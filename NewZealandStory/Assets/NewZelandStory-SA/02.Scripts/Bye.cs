using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bye : MonoBehaviour {
	public GameObject scorecanvas;

	// Use this for initialization
	void Start () {
		scorecanvas = GameObject.Find ("ScoreCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BYE()
	{
		//SceneManager.LoadScene("2-2.NS-Stage1");
		SceneManager.LoadScene("7.NS-finish");
		Destroy (scorecanvas);
	}

}
