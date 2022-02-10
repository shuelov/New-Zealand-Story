using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	public Text progressText;
	//private Animator anim;

	// Use this for initialization
	void Awake () {
		StartCoroutine (Load ());
		//anim = GetComponent<Animator> ();
	}
	
	IEnumerator Load()
	{
		yield return new WaitForSeconds (2);

		//AsyncOperation async=SceneManager.LoadSceneAsync("2.NS-Stage1");
		AsyncOperation async = SceneManager.LoadSceneAsync("1-2.NS-Story");
		while (!async.isDone)
		{

			progressText.text="LOADING";

			yield return true;
		}

	}
}
