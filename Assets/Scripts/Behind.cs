using UnityEngine;
using System.Collections;

public class Behind : MonoBehaviour
{


    public Transform Player;
    public Transform Camera;
    public Transform Object;
    public GameObject Phone;
    public Canvas UI;
    public Canvas UI2;
    public CharacterController Controller;
    public float distance;
    public GameObject move;
    public GameObject move2;
    private float _distance;


    // Use this for initialization
    void Start()
    {
        UI.gameObject.SetActive(false);
        if(UI2 != null)
            UI2.gameObject.SetActive(false);
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
                        if (UI2 != null)
                            UI2.gameObject.SetActive(true);
                      

                        if (Input.GetButtonDown("A"))
                        {
                            if (!PlayerAnimState.instance.isBehind) // 숨는중이아니면
                                StartCoroutine(DroopBehind()); // 수그려서 숨기
                        }
                    }

                    else
                    {
                        //문태그가 아니면 ui false

                        UI.gameObject.SetActive(false);
                        if (UI2 != null)
                            UI2.gameObject.SetActive(false);

                    }
                }
                else
                {

                    //물체에 닿은게없으면 ui false
                    UI.gameObject.SetActive(false);
                    if (UI2 != null)
                        UI2.gameObject.SetActive(false);
                }

                if(PlayerAnimState.instance.TState == PlayerAnimState.ActionState.Behind) // 숨기 상태면
                {
                     if (Input.GetButtonDown("A"))
                        {
                        if (!PlayerAnimState.instance.isBehind) // 숨는중이아니면
                            StartCoroutine(GearGush()); //기어나오기
                        }
                }

            }
            else
            {
               
                //거리안에 안들어옴면 ui false

                UI.gameObject.SetActive(false);
                if (UI2 != null)
                    UI2.gameObject.SetActive(false);
            }

            yield return null;
        }

    }

    public IEnumerator DroopBehind() // 수그려서 숨기
    {
        yield return null;
        PlayerAnimState.instance.isBehind = true; //숨는중
        float startTime = 3f;
        move2.active = false; // 무브
        if (PlayerAnimState.instance.TState != PlayerAnimState.ActionState.Behind)// 숨기 상태가 아니면
        {
 
            Controller.enabled = false;  // 컨트롤러 비활성
            Phone.GetComponent<ArmScript>().phoneOff(); //핸드폰 내리기
            PlayerAnimState.instance.TState = PlayerAnimState.ActionState.Behind; // 상태를 숨기로
            yield return new WaitForSeconds(1f); 
      
                                                                                 
          
            while (true)
            {
                startTime -= Time.deltaTime; // 시간감소
                Player.localScale = Vector3.Lerp(Player.localScale, new Vector3(0.1f,0.1f,0.1f),  Time.deltaTime); // 숙인다
                if (startTime < 0)  //시간다되면 break
                {
                    break;
                }
                yield return null;
            }

            move.active = true; // 무브!

        }
        PlayerAnimState.instance.isBehind = false;

    }
    public IEnumerator GearGush() // 기어나오기
    {
        yield return null;
        PlayerAnimState.instance.isBehind = true; //숨는중
        float startTime = 3f;
        move.active = false;
        move2.active = true; // 무브

        yield return new WaitForSeconds(2f);

        while (true)
        {
            startTime -= Time.deltaTime; // 시간감소
            Player.localScale = Vector3.Lerp(Player.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime); // 일어난다.
            if (startTime < 0)  //시간다되면 break
            {
                break;
            }
            yield return null;
        }

     
        PlayerAnimState.instance.TState = PlayerAnimState.ActionState.Idle; //기본으로
        
        Controller.enabled = true; // 컨트롤러 활성

        PlayerAnimState.instance.isBehind = false; 
    }

}


