using UnityEngine;
using System.Collections;

public class BehindCurtain : MonoBehaviour {


    public Transform Player;
    public Transform Camera;
    public Transform Object;
    public Animator animator;
    public GameObject Phone;
    public Canvas UI;
    public CharacterController Controller;
    public float distance;
    public GameObject move;
    public GameObject move2;
    private float _distance;


    // Use this for initialization
    void Start()
    {
        UI.gameObject.SetActive(false);
        StartCoroutine(CheckUIPossible()); //코루틴 실행
    }



    IEnumerator CheckUIPossible() // ui 띄울수 있는지
    {


        yield return null;

        while (true)
        {

            _distance = Vector3.Distance(Object.position, Player.position); // 물건과 플레이어간의 거리차이
            Ray ray = new Ray(Camera.position, Camera.rotation * Vector3.forward); //레이

            Debug.DrawRay(Camera.position, Camera.rotation * Vector3.forward * 10); // 카메라가 보는 광선을 그려줌

            RaycastHit hit;


            if (_distance <= distance) // 적당한 거리에 들어오면
            {
                if (Physics.Raycast(ray, out hit)) //광선과 물체에 닿으면
                {

                    if (hit.transform.gameObject.name == Object.name) //카메라와 오브젝트 충돌시
                    {
                        UI.gameObject.SetActive(true); //ui 띄어줌
             


                        if (Input.GetButtonDown("A"))
                        {
                            if (!PlayerAnimState.instance.isBehind) // 숨는중이아니면
                                StartCoroutine(CurtainBehind()); // 커텐 열고 숨기
                        }
                    }

                    else
                    {
                        //문태그가 아니면 ui false

                        UI.gameObject.SetActive(false);
                    

                    }
                }
                else
                {

                    //물체에 닿은게없으면 ui false
                    UI.gameObject.SetActive(false);
                  
                }

                if (PlayerAnimState.instance.TState == PlayerAnimState.ActionState.Behind) // 숨기 상태면
                {
              
                    if (Input.GetButtonDown("A"))
                    {
                     
                        if (!PlayerAnimState.instance.isBehind) // 숨는중이아니면
                            StartCoroutine(ExitCutain()); // 나오기
                    }
                }

            }
            else
            {

                //거리안에 안들어옴면 ui false

                UI.gameObject.SetActive(false);
         
            }

            yield return null;
        }

    }
    public IEnumerator CurtainBehind() // 커텐 열고 숨기
    {
        yield return null;
        PlayerAnimState.instance.isBehind = true; //숨는중
        move2.active = false; // 무브

        if (PlayerAnimState.instance.TState != PlayerAnimState.ActionState.Behind)// 숨기 상태가 아니면
        {
          
            Controller.enabled = false;  // 컨트롤러 비활성
            Phone.GetComponent<ArmScript>().phoneOff(); //핸드폰 내리기
            PlayerAnimState.instance.TState = PlayerAnimState.ActionState.Behind; // 상태를 숨기로
            yield return new WaitForSeconds(1f);

          
            animator.SetBool("behind",true); // 커텐열닫
            yield return new WaitForSeconds(2f);
            move.active = true; // 무브!

            yield return new WaitForSeconds(1f);

            animator.SetBool("behind", false); 
        }

        PlayerAnimState.instance.isBehind = false; //숨기끝
    }

    public IEnumerator ExitCutain() // 커텐 열고 나오기
    {
       
        PlayerAnimState.instance.isBehind = true; //숨는중

        move.active = false;
        animator.SetBool("behind", true); // 커텐열닫
        yield return new WaitForSeconds(3f);

        move2.active = true; // 무브!

        yield return new WaitForSeconds(1f);

        animator.SetBool("behind", false);

        PlayerAnimState.instance.TState = PlayerAnimState.ActionState.Idle; //기본으로

        Controller.enabled = true; // 컨트롤러 활성


        yield return null;
        PlayerAnimState.instance.isBehind = false; //숨기끝
    }


  
}
