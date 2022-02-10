using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mon1attck : MonoBehaviour {

	public PlayerLife killplayer;
    Rigidbody2D rid;
    float moveForce = 40f;
    bool isdead = false;
    float delaytime = 0.2f;
    float servivetime = 20f;
    float h = -1f;
    float timeflows;

    void Awake()
	{
		killplayer = GameObject.Find ("Tikki1").GetComponent<PlayerLife> ();
        rid = GetComponent<Rigidbody2D>();
        timeflows = 0f;
    }

	void Start()
	{
		//Destroy(gameObject, 1); //생성되고 1초후에 사라짐
        StartCoroutine(Timesflying());
        h = rid.velocity.x;
    }

    void Update()
    {
        if (!isdead)
        {
            if (timeflows > servivetime)
            {
                timeflows += Time.time;
                StopCoroutine(Timesflying());
                Dead();
            }
        }
    }

    IEnumerator Timesflying()
    {
        //Debug.Log(isdead);
        while (!isdead)
        {         
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
            yield return new WaitForSeconds(delaytime);
        }
    }

    void OnTriggerEnter2D (Collider2D col) 
	{
		// 만약에 몹 무기가 Player에게 Hit 되었을때...
		if (col.gameObject.tag == "Player") {
			//player죽음
			killplayer.kill ();

            // 몹 무기 삭제
            Dead();
        }

		// 그렇지 않고 무기가 벽이나 가시에 부딪혔다면 삭제
		else if(col.gameObject.tag == "Wall"||col.gameObject.tag == "Spike")
		{
            Dead();
            //Destroy (rigidbody2D);
        }
		//else 
		//	Destroy (gameObject,1);

	}

     void OnCollisionEnter2D(Collision2D collision)
    {
        // 만약에 몹 무기가 Player에게 Hit 되었을때...
        if (collision.gameObject.tag == "Player")
        {
            //player죽음
            killplayer.kill();

            // 몹 무기 삭제
            Dead();
        }

        // 그렇지 않고 무기가 벽이나 가시에 부딪혔다면 삭제
        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Spike")
        {
            Dead();
        }
    }

    void Dead()
    {
        isdead = true;
        Destroy(gameObject);
    }
    
}
