using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSortingLayerSet : MonoBehaviour {

	//파티클들의 sorting layer의 이름을 셋팅해야만 한다
	public string sortingLayername;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayername;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
