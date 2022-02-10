using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NSBtnCtrl : MonoBehaviour {


	public bool pressBtn;

	void Start () {

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update () {

		if (pressBtn) {
			ToStory ();
			pressBtn=false;
		}
	}

	public void ToStory()
	{
		StartCoroutine (changemap ());
	}
	IEnumerator changemap()
	{
		yield return new WaitForSeconds (0.8f);
		SceneManager.LoadScene("Loading");	

	}
}
