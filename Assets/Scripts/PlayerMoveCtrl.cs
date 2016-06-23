using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour {

    public Transform camera; // 메인카메라 객체
    public float speed = 3.0F; // 플레이어 기본 속도

    //  public float rotationSpeed = 100.0F;
    // Use this for initialization
    
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Transform>(); // 카메라 위치 컴포넌트 할당
    }


    void Update()
    {

        float Vertical = Input.GetAxis("Vertical") * speed; // 전진 키 입력시
        float L1 = Input.GetAxis("Horizontal"); // L1 버튼 입력시 
        float Jump = Input.GetAxis("Jump"); // 점프 버튼 입력시 

    

        //  float rotation = Input.GetAxis("Horizontal") * rotationSpeed;


        if (Vertical != 0f) // 전진 키가 눌릴 경우
        {
            GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Walk; // 플레이어 애니메이션 상태 걷기로
            speed = 3f;
            
            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {
                GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Run; // 플레이어 애니메이션 상태 뛰기로
                speed = 5f; // 속도 증가
            }
        }
        
        else // 아무 입력도 없을경우
        {
            GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Idle; //  플레이어 애니메이션 상태 가만히

        }

    

        if (Jump!= 0)
        {
            GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Jump; // 플레이어 애니메이션 상태 점프
            
        }

     
        Vertical *= Time.deltaTime;
        //    rotation *= Time.deltaTime;
        transform.Translate(0, 0, Vertical); // 앞으로 전진
        // transform.Rotate(0, rotation, 0);

        transform.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0); //카메라 회전에 따라  플레이어도 같이 회전
        

    }
}
