using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour 
{
	//파괴 옵션들
	public bool destroyOnAwake;			// 인스펙터에서 체크시 Awake() 에서 이 게임오브젝트의 해당 자식객체 소멸 또는 자기자신을 일정 시간 딜레이후 소멸
	public float awakeDestroyDelay;		// Awake() 에서 사용되는 소멸을 위한 딜레이 타임
	public bool findChild = false;		// 인스펙터에서 체크시 해당 자식게임오브젝트를 찾아서 소멸한다.(부모는 살아있되)
	public string namedChild;			// 인스펙터에서 설정 가능한 소멸될 자식객체의 이름 (문자열로 인스펙터에 넣으면 된다)
	
	
	void Awake ()
	{
		// 만약에 destroyOnAwake 가 true 일때 
		if(destroyOnAwake)
		{
			if(findChild)
			{
				// 이 게임오브젝트의 해당 자식객체 소멸
				Destroy (transform.Find(namedChild).gameObject);////'소문자시작:자기자신의' 검색범위(부모 포함 자식들까지 검색) 그 중 해당하는 오브젝트의 transform이 넘어옴
			}	//레퍼런스가 게임오브젝트형이 아니라 트랜스폼 형태의 참조값으로 넘어옴
			else
			{
				// 딜레이 후 게임오브젝트를 소멸 
				Destroy(gameObject, awakeDestroyDelay);
			}
		}
	}

	// 이 함수는 Animation Even로부터 호출될수있다
	void DestroyChildGameObject ()
	{
		// 이 게임오브젝트의 해당 이름의 자식객체가 존재 할 경우 그 child gameobject 소멸
		if(transform.Find(namedChild).gameObject != null)
			Destroy (transform.Find(namedChild).gameObject);
	}

	// 이 함수는 Animation Even로부터 호출될수있다
	void DisableChildGameObject ()
	{
		// 이 게임오브젝트의 해당 이름의 자식객체가 활성화중일 경우 그 child gameobject 소멸
		if(transform.Find(namedChild).gameObject.activeSelf == true)
			transform.Find(namedChild).gameObject.SetActive(false);
	}

	// 이 함수는 Animation Even로부터 호출될수있다
	void DestroyGameObject ()
	{
		// 이 게임오브젝트 삭제
		Destroy (gameObject);
	}
}
