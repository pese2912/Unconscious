using UnityEngine;
using System.Collections;

public class BehindCloset : MonoBehaviour {


    public Transform Player;
    public Transform Camera;
    public Transform Object;
    public Transform Object2;
    public GameObject Phone;
    public Canvas UI;
    public CharacterController Controller;
    public float distance;
    public GameObject move;
    public GameObject move2;
    private float _distance;
    public int state1;
    public int state2;



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
                                StartCoroutine(ClosetBehind()); // 벽장열고 숨기
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
                            StartCoroutine(ExitCloset()); //문열고 나오기
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



    public IEnumerator ClosetBehind() // 벽장문 열고 숨기
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
  

            StartCoroutine(FastOpenClose());//문열고 닫기
            yield return new WaitForSeconds(2f);

            move.active = true; // 무브!

        }
        PlayerAnimState.instance.isBehind = false; 

    }
    public IEnumerator ExitCloset() // 나오기
    {
        yield return null;
        PlayerAnimState.instance.isBehind = true; //숨는중
        move.active = false;
      

        yield return new WaitForSeconds(2f);

        StartCoroutine(FastOpenClose());//문열고 닫기
        yield return new WaitForSeconds(2f);
        move2.active = true; // 무브
        PlayerAnimState.instance.TState = PlayerAnimState.ActionState.Idle; //기본으로

        Controller.enabled = true; // 컨트롤러 활성

        PlayerAnimState.instance.isBehind = false; //숨는중
    }



    public IEnumerator FastOpenClose() //빨리 문을 열고 닫는다
    {
       
        float startTime = 3f;
        Quaternion original = Object.rotation; // 원래 회전값
        Quaternion original2 = Object2.rotation; // 원래 회전값


        while (true) //문열기
        {
            startTime -= Time.deltaTime; // 시간감소
            if (state1 == 1)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, -4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state1 == 2)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state1 == 3)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 1f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전

            if (state2 == 1)
                Object2.rotation = Quaternion.Slerp(Object2.rotation, new Quaternion(0f, -4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state2 == 2)
                Object2.rotation = Quaternion.Slerp(Object2.rotation, new Quaternion(0f, 4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state2 == 3)
                Object2.rotation = Quaternion.Slerp(Object2.rotation, new Quaternion(0f, 1f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전

            if (startTime < 0)  //시간다되면 break
            {
                break;
            }

            yield return null;
        }

        startTime = 5f;
        while (true) // 문닫기
        {
            startTime -= Time.deltaTime;
            Object.rotation = Quaternion.Slerp(Object.rotation, original, Time.deltaTime); // 원래  값으로 문회전
            Object2.rotation = Quaternion.Slerp(Object2.rotation, original2, Time.deltaTime); // 원래  값으로 문회전
            if (startTime < 0)
            {
                break;  // 시간 다되면 break
            }
            yield return null;
        }
    
    }



}
