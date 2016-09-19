using UnityEngine;
using System.Collections;
using System.Linq;

public class AI_Enemy : MonoBehaviour {

    public static AI_Enemy instance=null;
	public enum AI_EVENT_STATE{
		IDLE,
		PATROL,
		CHASE,
		ATTACK,
        PHOTOSHOTCH
	};




	public Animator ThisAnimator;
	public NavMeshAgent ThisAgent;
	public Transform ThisTransform;
	public Transform PlayerTransform;

    RaycastHit hit;


	private bool CanSeePlayer = false;
	public AI_EVENT_STATE CurrentState = AI_EVENT_STATE.IDLE;
	[SerializeField]
	private Transform[] WayPoints;
	//Compare Distance min
	private float DistEps=3.5f;

    private int idleIdx;
    private int patrolIdx;
    private int chaseIdx;
    private int attackIdx;
    private int photoIdx;


    private float distancePatrol = 5.0f;

	private float ChasedTime=5.0f;

	//public Animation ani;
	void Awake(){
        instance = this;
        idleIdx=Animator.StringToHash("Idle");
        patrolIdx=Animator.StringToHash("Patrol");
        chaseIdx=Animator.StringToHash("Chase");
        attackIdx=Animator.StringToHash("Attack");
        photoIdx=Animator.StringToHash("Photo");

		ThisAnimator = GetComponent<Animator> ();
		ThisAgent = GetComponent<NavMeshAgent> ();
		ThisTransform = transform;
		//플레이어 position을 어디에서 찿 아 야 하는가?
		PlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

			//use LINQ
        GameObject[] TempWaypoints =GameObject.FindGameObjectsWithTag ("WAYPOINTS");
		WayPoints = (from GameObject GO in TempWaypoints
		             select GO.transform).ToArray ();
		

	}
	void Start(){
       
	}

	// Update is called once per frame
	void Update () {
        Debug.DrawRay(ThisTransform.position, (ThisTransform.forward) * 20.0f, Color.green);
		Debug.DrawRay(ThisTransform.position, (ThisTransform.forward+ThisTransform.right) * 20.0f, Color.green);
		Debug.DrawRay(ThisTransform.position, (ThisTransform.forward+ThisTransform.right*-1) * 20.0f, Color.green);


		//assumption Enemy cannot see Player
		CanSeePlayer = false;
		//Do Box contain Player? 
	
		CanSeePlayer = HaveLineSightToPlayer (PlayerTransform);
	}


	//플레이어 와 적 사이에 가시성 이 있는가 , 중간에 벽이 있는
	private bool HaveLineSightToPlayer(Transform Player)
	{
        

        //가까이
        if (Vector3.Distance(Player.position, ThisTransform.position) < 10.0f)
        {
           // Debug.Log("Closer");
            return true;
        }

        if(PlayerAnimState.instance.TState==PlayerAnimState.ActionState.Behind)
        {
           
           // Debug.Log("PlayerBeHind");
            return false;

        }
        if (PlayerAnimState.instance.isRun)
        {
           
            //  Debug.Log("PlayerRun");
            return true;
        }
        if (Physics.Raycast(ThisTransform.position, ThisTransform.forward, out hit, 20.0f, 1 << LayerMask.NameToLayer("PLAYER")))
        {
			
            if (hit.collider.tag == "Player")
            {
               Debug.Log("RayCast");
                return true;
            }
        }
		if (Physics.Raycast(ThisTransform.position, (ThisTransform.forward+ThisTransform.right), out hit, 20.0f, 1 << LayerMask.NameToLayer("PLAYER")))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log("RayCast");
                return true;
            }
        }
		if (Physics.Raycast(ThisTransform.position, (ThisTransform.forward+ThisTransform.right*-1), out hit, 20.0f, 1 << LayerMask.NameToLayer("PLAYER")))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log("RayCast");
                return true;
            }
        }
       
        return false;
	}


	public IEnumerator State_Idle(){
		//현제 상태 IDlE;
		CurrentState = AI_EVENT_STATE.IDLE;
        //START IDle Animation
       // Debug.Log("IDLE");
        ThisAnimator.SetTrigger (idleIdx);
        //Nav Mesh moving stop
		ThisAgent.Stop();


		
		


		while (CurrentState == AI_EVENT_STATE.IDLE) {
            
			if (CanSeePlayer) {
				StartCoroutine (State_Chase ());
				yield break;
			}
			//wait untill next frame
			yield return null;
		}
	}

	public void OnIdleAnimCompleted(){
		
		StopAllCoroutines ();
		StartCoroutine (State_Patrol ());
	}

	public IEnumerator State_Patrol(){
		//Debug.Log ("Patrol");
		//set Current State
		CurrentState = AI_EVENT_STATE.PATROL;
		//start Animation "Patrol"
		Transform RandomDest = WayPoints [Random.Range (0, WayPoints.Length)];
		//Move to RandomDest
		ThisAgent.speed=3f;

		ThisAgent.SetDestination (RandomDest.position);
		ThisAgent.Resume ();
		
        ThisAnimator.SetTrigger (patrolIdx);
		//select Temp Destination


		while (CurrentState == AI_EVENT_STATE.PATROL) {
            
           
			if (CanSeePlayer) {
				StopAllCoroutines ();
				StartCoroutine (State_Chase ());
				yield break;
			}
            if (Vector3.Distance(ThisTransform.position, RandomDest.position) <= distancePatrol)
            {
                //Debug.Log("Distance");
                StartCoroutine(State_Idle());
                yield break;
            }
            
            yield return new WaitForSeconds(0.3f);
		}

	}

	public IEnumerator State_Chase(){
		//Debug.Log ("Chase");
		CurrentState = AI_EVENT_STATE.CHASE;

        ThisAnimator.SetTrigger (chaseIdx);
        ThisAgent.speed=8f;
		while (CurrentState == AI_EVENT_STATE.CHASE) {
           
			ThisAgent.SetDestination (PlayerTransform.position);
			ThisAgent.Resume ();

			float ElapsedTime = 0.0f;
            while (!CanSeePlayer) {
                
					ElapsedTime += Time.deltaTime;
					ThisAgent.SetDestination (PlayerTransform.position);
					ThisAgent.Resume ();
					yield return null;
					if (ElapsedTime >= ChasedTime) {
						if (!CanSeePlayer) {
							//Debug.Log ("Chase to Idle");
							StartCoroutine (State_Idle ());
							yield break;
							
						} else
							break;
					}
			}
			if (Vector3.Distance (ThisTransform.position, PlayerTransform.position) <= DistEps) {
				StartCoroutine (State_Attack ());
				yield break;
			}
            yield return new WaitForSeconds(0.2f);
		}
	}
	public IEnumerator State_Attack(){
		//Debug.Log ("Attack");

		CurrentState = AI_EVENT_STATE.ATTACK;
        ThisAnimator.SetTrigger (attackIdx);
		ThisAgent.Stop ();
       
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(State_Chase());
      

		yield return null;
	}
    public IEnumerator State_Picture(){
        
        ThisAgent.Stop();
        CurrentState = AI_EVENT_STATE.PHOTOSHOTCH;
        Vector3 _tempPictureTrans = PlayerTransform.position;
        ThisAnimator.SetTrigger (photoIdx);
        ThisAgent.speed=8f;
        ThisAgent.SetDestination (_tempPictureTrans);
        ThisAgent.Resume ();

        while (CurrentState == AI_EVENT_STATE.PHOTOSHOTCH)
        {
            if (CanSeePlayer)
            {
                StartCoroutine(State_Chase());
                yield break;
            }
           
            if (Vector3.Distance(ThisTransform.position, _tempPictureTrans) <= distancePatrol)
            {
               
                StartCoroutine(State_Idle());
              
                yield break;
            }
            yield return null;
        }



        
    }
    public void OnTakePicture(){
        if (CurrentState == AI_EVENT_STATE.IDLE || CurrentState == AI_EVENT_STATE.PATROL)
        {
            StopAllCoroutines();
            StartCoroutine(State_Picture());
        }
    }
   
}
