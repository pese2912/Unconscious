using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour {


    public Transform camera; // 메인카메라 객체
    public float speed = 1F; // 플레이어 기본 속도
    public GameObject Player;

    public PlayerAnimState state; // 현재 플레이어 애니메이션 상태

    //  public float rotationSpeed = 100.0F;
    // Use this for initialization
    
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Transform>(); // 카메라 위치 컴포넌트 할당
        Player = GameObject.Find("Player");
        state = Player.GetComponent<PlayerAnimState>();
      
    }


    void Update()
    {

        float Vertical = Input.GetAxis("Vertical") * speed; // 전진 키 입력시
        float L1 = Input.GetAxis("L1"); // L1 버튼 입력시 
        float Jump = Input.GetAxis("Y"); // 점프 버튼 입력시 

    

        //  float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

     
        if (Vertical != 0f) // 전진 키가 눌릴 경우
        {
            state.animState = PlayerAnimState.AnimState.Walk; // 플레이어 애니메이션 상태 걷기로
            speed = 1.5f;
            
            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {
                state.animState = PlayerAnimState.AnimState.Run; // 플레이어 애니메이션 상태 뛰기로
                speed = 3f; // 속도 증가
            }
        }
        
        else // 아무 입력도 없을경우
        {
            state.animState = PlayerAnimState.AnimState.Idle; //  플레이어 애니메이션 상태 가만히

        }

    

        if (Jump!= 0)
        {
            state.animState = PlayerAnimState.AnimState.Jump; // 플레이어 애니메이션 상태 점프
            
        }


        if (Input.GetButtonDown("X")) // x키 누를시
        {
            if (state.actState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
            {
                state.actState = PlayerAnimState.ActionState.PhoneDown; // 폰 내리기
            }

            else if (state.actState == PlayerAnimState.ActionState.PhoneDown) // 폰이 아래 있을 경우에만
            {
                state.actState = PlayerAnimState.ActionState.PhoneUp; // 폰 올리기
            }
            else 
            {
                state.actState = PlayerAnimState.ActionState.PhoneUp; // 폰 올리기
            }
        }
        
        if (Input.GetButtonDown("A")) //a키 누를시
        {
            if (state.actState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
            {
                state.actState = PlayerAnimState.ActionState.LightOn; // 라이트 켜기
            }

            else if (state.actState == PlayerAnimState.ActionState.LightOn) // 라이트 켜있을 경우
            {
                state.actState = PlayerAnimState.ActionState.PhoneUp; // 라이트 끄기
            }
        }
        
        if(Input.GetButtonDown("B")) // b키 누를시
        {
            if (state.actState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
            {
                state.actState = PlayerAnimState.ActionState.PhotoMode; // 사진 모드
            }

            else if (state.actState == PlayerAnimState.ActionState.PhotoMode) // 사진 모드일경우
            {
                state.actState = PlayerAnimState.ActionState.PhotoShot; // 사진 찍기
            }
        }
        

     
        Vertical *= Time.deltaTime;
        //    rotation *= Time.deltaTime;
        transform.Translate(0, 0, Vertical); // 앞으로 전진
        // transform.Rotate(0, rotation, 0);

        transform.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0); //카메라 회전에 따라  플레이어도 같이 회전
        

    }
}


