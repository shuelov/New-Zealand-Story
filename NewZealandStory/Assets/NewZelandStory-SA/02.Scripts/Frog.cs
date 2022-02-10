using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

    public float height=10f;
    public int count=0;
    public Transform frog;

	// Use this for initialization
	void Awake () {
        frog = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        StartCoroutine(JumpLoop());

	}

    IEnumerator JumpLoop()
    {
       // if (count < 3)
       // {
         yield return new WaitForSeconds(1);
           GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, height));
        
        // }

        //  else

    }

}
