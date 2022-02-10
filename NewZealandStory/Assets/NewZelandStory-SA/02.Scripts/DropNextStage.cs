using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropNextStage : MonoBehaviour {

	public GameObject[] DropItems;          //아이템 프리팹들 담겨있는 배열
	public MonPig enemypig;                  //MonPig 스크립트 받아옴


	public bool drop = false;                       //떨궜는지

	public int randomIndex;                     //랜덤의 수의 인덱스가 가르키는 아이템이 떨궈짐
	public int ItemIndex;                           //플레이어가 먹으려는 아이템이 무엇인지(인덱스)

	//drop할 좌표
	public float dropPosX;
    public float dropPosY;

    public MonpigThrow check;

    //과일에 collidor 추가하고 만약 티키가 닿으면 anim.settrigger("getpoint")하고 스코어에 점수 +=하기 
    //스코어 점수올리는거 enemy스크립트참조.포인트는 Item의 point변수 가져오고
    //트리거 getpoint되면 점수애니메이션실행되고 과일 spriterenderer사라지게하기. 폭탄일경우 티키가 바꿔끼게

    void Awake()
    {
        //레퍼런스(참조)들을 셋팅 
        enemypig = transform.root.GetComponent<MonPig>();
    }

    public void Update() {

        if (enemypig.dead==true)
		{
			if (drop == false) {

                //drop할 좌표받아오기
				dropPosX = enemypig.lastpositionX;
                dropPosY = enemypig.lastpositionY;

			   StartCoroutine (DropTheItem ());
				drop = true;
			}
		}
	}


	public IEnumerator DropTheItem()
	{
 
		yield return new WaitForSeconds (0);
		
		Vector3 dropPos = new Vector3 (dropPosX, dropPosY, 0f);

		randomIndex = Random.Range (0, DropItems.Length);							//랜덤으로 뽑기

		if (randomIndex==10)
			yield return null;
				
		else
			Instantiate (DropItems [randomIndex], dropPos, Quaternion.identity);	//아이템생성


	}

}
