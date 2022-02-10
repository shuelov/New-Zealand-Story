using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	void Awake ()
	{
		{
			//이 오브젝트는 씬 전환시 사라지지 않음
			DontDestroyOnLoad (this.gameObject);
			//Application.LoadLevel ("scLobby");
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
