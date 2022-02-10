using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	[Header("보스몹 위치")]
	public Transform pos;
	public Animator anim;

	public bool moving;
	public bool nonmoving;
	public float tiiime=0;
	public float tme=0;

	public int a=0;
	public int b=0;
	public int c=0;
	public int d=0;

	public bool one;
	public bool two;
	public bool three;
	public bool four;

	public bool open;

	public GameObject BInside;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		pos = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(ToRight());
	}

	// Update is called once per frame
	void Update () {
		if (!moving) {
			
			if (tiiime < Time.time) {
				//StartCoroutine (ToRight ());
				//movetime +=  Time.deltaTime;
	//			tiiime += 1f;
	//			moving = true;
	//			nonmoving = true;
			//	Debug.Log ("movewhale");
			}
		}

		if (nonmoving) {
			
			if (tme< Time.time) {
				//nonmovetime +=  Time.deltaTime;
	//			tme += 1f;
	//			moving = false;
	//			nonmoving = false;
				//Debug.Log ("nonmovewhale");

			}
		}

	}

	//[ContextMenu("ToRight")]
	IEnumerator ToRight()
	{
		if (!one) {
			yield return new WaitForSeconds (0.2f);
			pos.position = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z);
			Vector3.Lerp (transform.position, pos.position, Time.deltaTime * 10.0f);
			a++;
			if (a > 2) {
				one = true;
				two = true;
			}
			else
				Start();
		}
		if (two) {
			yield return new WaitForSeconds (0.2f);
			pos.position = new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z);
			Vector3.Lerp (transform.position, pos.position, Time.deltaTime * 10.0f);
			b++;
			if (b > 2) 
			{
				two = false;
				three = true;
			}
			else
				Start();
		}
		if (three) {
			yield return new WaitForSeconds (0.2f);
			pos.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
			Vector3.Lerp (transform.position, pos.position, Time.deltaTime * 10.0f);
			c++;
			if (c > 3) 
			{
				three = false;
				four = true;
			}
			else
				Start();
		}
		if (four) {
			yield return new WaitForSeconds (0.2f);
			pos.position = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
			Vector3.Lerp (transform.position, pos.position, Time.deltaTime * 10.0f);
			d++;
			if (d > 5) 
			{
				four = false;
				anim.SetTrigger ("open");
				open = true;
			}
			else
				Start();
		}
		//Debug.Log ("movewhale?");
		yield return null;
	}

	void OnTriggerEnter2D (Collider2D CCC)
	{
		if (CCC.gameObject.tag == "Player")
		{
			StartCoroutine(TurnInside());
		}
	}

	IEnumerator TurnInside()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(gameObject);
		BInside.SetActive(true);
		Transform Tikki = GameObject.Find("TikkiBoss").GetComponent<Transform>();
		GameObject.Find("TikkiBoss").GetComponent<PlayerCtrl3>().Change = true;
		Tikki.position = new Vector3(-1.4f, -2.49f, 0f);

	}
}
