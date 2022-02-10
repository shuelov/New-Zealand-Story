using UnityEngine;
using System.Collections;

public class MonsterWeapons : MonoBehaviour 
{
	public float moveSpeeds=-1f;//몬스터무기의 이동속도 '-'면 왼쪽으로 이동
	public Rigidbody2D weapon;
	public Enemy enemies;//Enemy script를 위한 Reference
	private float sp;

	void Awake()
	{
		//레퍼런스 셋팅
		enemies=transform.root.GetComponent<Enemy>();

	}
	public void Startth()
	{
		StartCoroutine(Shoot ());
	//	Debug.Log ("pretty");
	}

	IEnumerator Shoot()
	{
		
		if (!enemies.dead&&enemies.ATTACK) {
				
				//if (Time.time > sp) {
				//만약 몬스터가가 오른쪽 방향이라면
				if (enemies.dirRight) {
					//오른쪽 방향으로 무기 생성하고 오른쪽으로 무기의 속도를 셋팅
					Rigidbody2D bulletInstance = Instantiate (weapon, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (moveSpeeds, 0);
				//Debug.Log ("wtf");

					} else {
					//그렇지 않으면 왼쪽 방향으로 무기 생성하고 왼쪽으로 무기의 속도를 셋팅
					Rigidbody2D bulletInstance = Instantiate (weapon, transform.position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (-moveSpeeds, 0);
				//Debug.Log ("wth");
				}
				//sp = Time.time + (30f * Time.deltaTime); //deltatime(프레임당 걸리는 시간)
				yield return new WaitForSeconds(4); 
				Startth();

			}
		}

	}


