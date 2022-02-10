using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Retry : MonoBehaviour {

	public void Again()
	{
		StartCoroutine (Restart ());
	}
	IEnumerator Restart()
	{
		yield return new WaitForSeconds(0.2f);
		SceneManager.LoadScene ("2.NS-Stage1");
	}

	public void Quit()
	{
		Application.Quit();
	}

}

