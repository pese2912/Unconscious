using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    public Transform Player;
    public Transform Camera;
    public Transform Object;
    public int state1;
    public Canvas UI;
    public Canvas UI2;
    public float distance;
    float lastClickTime = 0;

    
    float catchTime = 0.25f;
    private float _distance;
    
	// Use this for initialization
	void Start () {

        UI.gameObject.SetActive(false);
        UI2.gameObject.SetActive(false);
        StartCoroutine(CheckUIPossible()); //코루틴 실행
 

    }

    public IEnumerator SlowOpenClose() //천천히 문을 열고 닫는다
    {
        PlayerAnimState.instance.isSlowOpenDoor = true;
        float startTime = 8f; 
        Quaternion original = Object.rotation; // 원래 회전값
        yield return new WaitForSeconds(0.1f); // 0.1초 후
        
        while (true) //문열기
        {
            startTime -= Time.deltaTime; // 시간감소
            if(state1 == 1)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, -4f, 0f, 3f), Time.deltaTime*0.1f); //문 회전
            else if(state1 == 2)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 4f, 0f, 3f), Time.deltaTime * 0.1f); //문 회전
            else if (state1 == 3)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 1f, 0f, 3f), Time.deltaTime * 0.1f); //문 회전
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

            if (startTime < 0)
            {
                break;  // 시간 다되면 break
            }
            yield return null;
        }
        PlayerAnimState.instance.isSlowOpenDoor = false;
    }

    

    public IEnumerator FastOpenClose() //빨리 문을 열고 닫는다
    {
        PlayerAnimState.instance.isFastOpenDoor = true;
        float startTime = 5f;
        Quaternion original = Object.rotation; // 원래 회전값
      

        while (true) //문열기
        {
            startTime -= Time.deltaTime; // 시간감소
            if (state1 == 1)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, -4f, 0f, 3f), Time.deltaTime*0.5f); //문 회전
            else if (state1 == 2)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state1 == 3)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 1f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
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

            if (startTime < 0)
            {
                break;  // 시간 다되면 break
            }
            yield return null;
        }
        PlayerAnimState.instance.isFastOpenDoor = false;
    }


    public IEnumerator MonsterOpenClose() //빨리 문을 열고 닫는다
    {
    
        float startTime = 5f;
        Quaternion original = Object.rotation; // 원래 회전값


        while (true) //문열기
        {
            startTime -= Time.deltaTime; // 시간감소
            if (state1 == 1)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, -4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state1 == 2)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 4f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
            else if (state1 == 3)
                Object.rotation = Quaternion.Slerp(Object.rotation, new Quaternion(0f, 1f, 0f, 3f), Time.deltaTime * 0.5f); //문 회전
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

            if (startTime < 0)
            {
                break;  // 시간 다되면 break
            }
            yield return null;
        }
     
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
                    if (hit.transform.gameObject.name == Object.name) //카메라와 문 태그 충돌시
                    {
                        UI.gameObject.SetActive(true); //ui 띄어줌
                        UI2.gameObject.SetActive(true); //ui 띄어줌
                    

                        if (Input.GetButtonDown("A"))
                        {
                       
                            if (Time.time - lastClickTime < catchTime)
                            {
                                //double click
                                if (Object != null && !PlayerAnimState.instance.isFastOpenDoor)
                                    StartCoroutine(FastOpenClose()); // 첫번째 문 열닫
                             

                            }
                            else
                            {
                                //normal click
                                if (Object != null && !PlayerAnimState.instance.isSlowOpenDoor)
                                    StartCoroutine(SlowOpenClose()); // 첫번째 문 열닫
                          

                            }
                         
                            lastClickTime = Time.time;
                        }
                    }

                    else
                    {
                        
                        //문태그가 아니면 ui false
                       
                        UI.gameObject.SetActive(false);
                        UI2.gameObject.SetActive(false);

                    }
                }
                else
                {
                  
                    //물체에 닿은게없으면 ui false
              
                    UI2.gameObject.SetActive(false);
                    UI.gameObject.SetActive(false);
                  
                }

            }
            else
            {
            
                //거리안에 안들어옴면 ui false
           
                UI.gameObject.SetActive(false);
                UI2.gameObject.SetActive(false);
            }

            yield return null;
        }

    }

	public void MonsterOpenDoor(){
		StartCoroutine(MonsterOpenClose());
	}
}
