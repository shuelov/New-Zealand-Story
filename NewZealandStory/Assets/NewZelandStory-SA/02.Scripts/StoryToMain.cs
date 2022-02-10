using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StoryToMain : MonoBehaviour {
	
	VideoPlayer video;
	AudioSource audios;

	void Awake()
	{
		video = GetComponent<VideoPlayer> ();
		audios = GetComponent<AudioSource> ();
		PlayVideo ();
	}
	public void PlayVideo()
	{
		StartCoroutine (playVideo ());
	}

	IEnumerator playVideo()
	{
		//동영상준비중
		while (!video.isPrepared) 
		{
			yield return new WaitForSeconds(1);
		}
	
		//영상 준비되었다면 재생
		video.Play ();
		audios.Play ();
	
		yield return new WaitForSeconds(8);
		//게임시작
		SceneManager.LoadScene ("2.NS-Stage1");
	}

}
