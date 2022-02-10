using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {


	public float timeToComplete = 3;

	//테스트 로직
	public Material[] matTest1;
	public Material matTest2;

	//*보통 생각하는 해결방법 (틀린 것)
	//public Material matTest2;

	//gameObject.GetComponent<Renderer>().materials=matTest2;
	//안되는 이유: Rederer자체의 materials는 주소값을 가르키는 instance이므로 
	//아래와 같이 풀어야 함

	//테스트 로직(progress진행되기 전에 실행되어야하므로 start함수 초기에 넣기)
	//public Material[] matTest1; //matTest1의 배열을 생성. 
	//public Material matTest2;   //progress를 담을 matTest2 변수 생성.

	//테스트 로직
	//matTest1 = gameObject.GetComponent<Renderer>().materials; //matTest1에 materials배열을 가져옴. 
	//플레이하면 matTest1의 size가 materials 크기대로 들어오는 것을 볼 수 있음
	//matTest1[0] = matTest2;	//matTest1[0]를 matTest2(progress)를 담게함
	//gameObject.GetComponent<Renderer>().materials = matTest1;//rederer의 materials는 자체 배열이 아니라 
	//matTest2를 0번째 인덱스에 담고있는 matTest1 배열을 가르키게됨. 실시간으로 0번째 인덱스의 progress실행가능

	// Use this for initialization
	void Start () {

		//테스트 로직
		matTest1 = gameObject.GetComponent<Renderer>().materials;
		matTest1[0] = matTest2;
		gameObject.GetComponent<Renderer>().materials = matTest1;


		// progress 코루틴 함수 시작
		StartCoroutine(RadialProgress(timeToComplete));
	}

	IEnumerator RadialProgress(float time)
	{
		float rate = 1 / time;
		float i = 0;
		while (i < 1)
		{
			i += Time.deltaTime * rate;
			//gameObject.GetComponent<Renderer>().material.SetFloat("_Progress", i);  //해당 컴포넌트의 renderer에 접근. SetFloat함수에 의해 메인 material의 progress값을 i로 셋팅함
			gameObject.GetComponent<Renderer>().materials[0].SetFloat("_Progress", i);
			//gameObject.GetComponent<Renderer>().materials[1].SetFloat("_Progress", i);

			yield return 0;
		}
	}
}