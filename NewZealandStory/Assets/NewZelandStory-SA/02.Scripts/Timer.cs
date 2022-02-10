using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	public AudioSource audios;
	public PlayerLife HurryUp;
	private AudioSource backSnd;


	public void Start () {
		audios = GetComponent<AudioSource> ();
		HurryUp = GameObject.Find("Tikki1").GetComponent<PlayerLife>();
		backSnd = GameObject.Find("stage1map").GetComponent<AudioSource>();
		StartCoroutine (TickTock ());
	}

	IEnumerator TickTock()
	{
		yield return new WaitForSeconds (30);
		backSnd.enabled = false;
		audios.Play();
		yield return new WaitForSeconds(15);
		HurryUp.direct = true;
		HurryUp.kill();
	}

}
