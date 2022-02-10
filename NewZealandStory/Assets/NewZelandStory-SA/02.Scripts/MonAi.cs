using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//외부노출
[System.Serializable]
public class Anim
{
	public AnimationClip Idle;
	public AnimationClip Move;
	public AnimationClip Trace;
	public AnimationClip Attack;
	public AnimationClip Dead;

}

public class MonAi : MonoBehaviour {


	[Header("Animation")]
	public Anim anims;	//위에 클래스명

	//public Animation _anim;
	public Animator ani;

	AnimationState animState;	

	private NavMeshAgent myTraceAgent; //Nav Mesh Agent 연결

	private Transform myTr;//몬스터 위치 연결

	//private bool traceObject;

	private bool traceAttack;

	[SerializeField]private GameObject[] players;  //몬스터가 플레이어 정보 다 가져옴

	[SerializeField]private Transform playerTarget;

	public float dist1;//플레이어공격

	public bool dead; //몹 죽었는지 아닌지

	public float lastpositionX;
	public float lastpositionY;
	public Transform deadposition;

	[SerializeField] private bool isHit;

	/*[열거형]*/
	//현재 상태
	public enum MODE_STATE{IDLE=1, TRACE, ATTACK, MOVE, DIE};

	//몬스터 종류
	public enum MODE_KIND{ENEMYPIG=1,ENEMYANGRY,ENEMYAUCH,ENEMYFROG,ENEMYBIRD};//몬스터마다 속도나 다른 설정다르게 할 수 있음

	[Header("STATE")]
	public MODE_STATE enemyMode = MODE_STATE.IDLE;

	[Header("SETTING")]
	public MODE_KIND enemyKind = MODE_KIND.ENEMYPIG;

	[Space(10)]

	[Header("몬스터 인공지능")]

	[Space(5)]

	//마우스로 대고있으면 설명나옴(tool tip)
	[Tooltip("몬스터의 HP")]
	[Range(0,1)]public int hp=1;

	[Tooltip("몬스터의 속도")]
	[Range(1f,2f)]public float speed= 2f;


	[Tooltip("몬스터 추적거리!!!")]
	[Range(3f, 5f)] [SerializeField] float traceDist = 4f;

	[Tooltip("몬스터 공격거리!!!")]
	[Range(1f, 3f)] [SerializeField] float attackDist = 3f;

	void Awake()
	{
		//레퍼런스할당
		myTraceAgent = GetComponent<NavMeshAgent> ();
		//_anim = GetComponent<Animation> ();//Monster의 애니메이션가져옴
		ani=GetComponent<Animator> ();
		myTr = GetComponent<Transform> ();//자기자신의 transform연결}
		deadposition = GetComponent<Transform> ();
	}
		IEnumerator Start () {

			//_anim.clip=anims.Idle;
			//_anim.Play();
			playerTarget=players[0].transform;
			myTraceAgent.SetDestination(playerTarget.position);

			StartCoroutine (ModeSet ());//정해진 시간간격으로 AI변화상태셋팅
			StartCoroutine (ModeAction ());//몹 상태변화에 따라 일정행동 수행
			StartCoroutine (this.TargetSetting());//일정간격으로 주변의 가장 가까운 Base와 플레이어셋팅
			yield return null;
		}

		/*
     *  Enemy의 변화 상태에 따라 일정 행동을 수행하는 코루틴
     */
		IEnumerator ModeSet()
		{
		while (!dead)
			{
				yield return new WaitForSeconds(0.2f);

				//자신과 Player의 거리 셋팅 
			float dist = Vector3.Distance(myTr.position, playerTarget.position);

				// 순서 중요
				if (isHit)  //공격 받았을시
				{
					enemyMode = MODE_STATE.DIE;
				}
				else if (dist <= attackDist) // Attack 사거리에 들어왔는지 ??
				{
					enemyMode = MODE_STATE.ATTACK; //몬스터의 상태를 공격으로 설정 
				}
				else if (traceAttack)  // 몬스터를 추적중이라면...
				{
					enemyMode = MODE_STATE.TRACE; //몬스터의 상태를 추적으로 설정
				}
				else if (dist <= traceDist) // Trace 사거리에 들어왔는지 ??
				{
					enemyMode = MODE_STATE.TRACE; //몬스터의 상태를 추적으로 설정 
				}
				else
				{
					enemyMode = MODE_STATE.IDLE; //몬스터의 상태를 idle 모드로 설정 
				}
			}
		}

		/*
	 * Enemy의 상태 변화에 따라 일정 행동을 수행하는 코루틴
	 */

		IEnumerator ModeAction()
		{
		while (!dead)
			{
				switch (enemyMode)
				{
				//Enemy가 Idle 상태 일때... 
				case MODE_STATE.IDLE:

					//네비게이션 멈추고 (추적 중지)
					myTraceAgent.isStopped = true;

					//_anim.CrossFade(anims.Idle.name, 0.3f);
					
					break;
					//Enemy가 Trace 상태 일때... 
				case MODE_STATE.TRACE:

					// 네비게이션 재시작(추적)
					myTraceAgent.isStopped = false;
					// 추적대상 설정(플레이어)
					myTraceAgent.destination = playerTarget.position;

					// 네비게이션의 추적 속도를 현재보다 1.5배
					myTraceAgent.speed = speed * 1.5f;

					//애니메이션 속도 변경
					//_anim[anims.Move.name].speed = 1.5f;

					//run 애니메이션 
					//_anim.CrossFade(anims.Move.name, 0.3f);

					break;

					//공격 상태
				case MODE_STATE.ATTACK:

					//사운드 (공격)

					//추적 중지(선택사항)
					//네비게이션 멈추고 (추적 중지) 
					myTraceAgent.isStopped = true;

					//공격할때 적을 봐라 봐야함 
					// myTr.LookAt(traceTarget.position); // 바로 바라봄
					//Quaternion enemyLookRotation = Quaternion.LookRotation(playerTarget.position - myTr.position); // - 해줘야 바라봄  
					//myTr.rotation = Quaternion.Lerp(myTr.rotation, enemyLookRotation, Time.deltaTime * 10.0f);

	
					//_anim.CrossFade(anims.Attack.name, 0.3f);
					
					break;
					//이동 상태 
				case MODE_STATE.MOVE:

					// 네비게이션 재시작(추적)
					myTraceAgent.isStopped = false;
					// 추적대상 설정(로밍장소)
					myTraceAgent.destination = playerTarget.position;

			
						// 네비게이션의 추적 속도를 현재보다 1.2배
						myTraceAgent.speed = speed * 1.2f;

						//애니메이션 속도 변경
						//_anim[anims.Move.name].speed = 1.2f;

						//run 애니메이션 
						//_anim.CrossFade(anims.Move.name, 0.3f);

	
						break;
				//이동 상태 
				case MODE_STATE.DIE:

					dead = true;
					ani.SetTrigger ("dead");
						break;

				}

				yield return null;
			}
		}

		// MODE_STATE.surprise 에서 호출 되는 함수 (이거없으면 계속 으르렁됨)
		IEnumerator TraceObject()
		{
			yield return new WaitForSeconds(2.5f);
			traceAttack = true;

			yield return new WaitForSeconds(5.5f);
			traceAttack = false;
			//traceObject = false;
		}


		// 자신과 가장 가까운 적을 찾음...플레이어가 베이스보다 우선순위가 높게 셋팅 추가
		IEnumerator TargetSetting()
		{
		while (!dead)
			{

				yield return new WaitForSeconds(0.2f);

				// 자신과 가장 가까운 플레이어 찾음
				players = GameObject.FindGameObjectsWithTag("Player");

				//플레이어가 있을경우 
				if (players.Length != 0)
				{
				Debug.Log ("findtikkiposition");
					playerTarget = players[0].transform;
					dist1 = (playerTarget.position - myTr.position).sqrMagnitude;//'sqrMagnitude':가장빠른길.정확도 떨어짐
					foreach (GameObject _players in players)
					{
						if ((_players.transform.position - myTr.position).sqrMagnitude < dist1)//더 가까운 사람 찾으면
						{
							playerTarget = _players.transform;//그 사람을 타겟으로 가짐
							dist1 = (playerTarget.position - myTr.position).sqrMagnitude;//그 사람과의 거리 셋팅
						}
					}
				}

		}
	}

	public void Update (){

		if (!dead) {
			lastpositionX = deadposition.position.x;
			lastpositionY = deadposition.position.y;
		}
	}

		//콘텍스트 메뉴에 함수 FunStart등록
		//플레이 중이 아니더라도 함수 실행해서 확인할 수 있음
		[ContextMenu("FuncStart")]
		void FunStart()
		{
			Debug.Log ("FuncStart");
			//Debug.Log (speed);
		}


}
