using UnityEngine;
using System.Collections;
using Rand=UnityEngine.Random; 

public class Snow : MonoBehaviour 
{
	public float z;

	public bool st;

	float Y;
	float delY;

	public Rigidbody2D yap;

	public float num;

	// 이 함수는 Animation Even로부터 호출될수있다
	void Awake ()
	{
		//Debug.Log ("snowawake");
		yap=GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D (Collision2D col)
	{

		if (col.gameObject.tag == "Ground"||col.gameObject.tag == "Spike") {
			Destroy (gameObject);
			//Debug.Log ("snowcollision");
		}

		if (col.gameObject.tag == "Player")
		{
			col.transform.root.GetComponent<PlayerLifeBOSS>().kill();
			Destroy(gameObject);
		}

			if (col.gameObject.tag == "Wall") 
		{
			st = true;
			Destroy (gameObject);
		}

	}

	void Update(){


		if (!st) {
			z += Time.deltaTime * 10;
			transform.rotation = Quaternion.Euler (0, 0, z);

				

			if(Time.deltaTime > 1){
				delY = transform.position.y;
			}
			else
				Y = transform.position.y;

			if (delY - Y > 0) 
			{
				yap.AddForce (new Vector2 (0, 0));
			}

			else
				
				num=Rand.Range(7,12);
				yap.AddForce (new Vector2 (num, 0));
						

		}

		if(st)
			transform.rotation = Quaternion.Euler (0, 0, 0);
	
	}

}
