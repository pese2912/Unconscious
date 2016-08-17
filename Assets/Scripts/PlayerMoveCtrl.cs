using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour
{

    public Transform camera; // 메인카메라 객체
    public float speed = 1F; // 플레이어 기본 속도
    public GameObject Player;
    public float speedRotation = 50F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public int evidenceCount = 0;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public PlayerAnimState state; // 현재 플레이어 애니메이션 상태
    public AudioSource walkSound;

    void Start()
    {
      
        controller = GetComponent<CharacterController>();
        camera = GameObject.Find("Camera").GetComponent<Transform>(); // 카메라 위치 컴포넌트 할당
        Player = GameObject.Find("Player");
        state = Player.GetComponent<PlayerAnimState>();
        walkSound = GetComponent<AudioSource>(); // 걸음소리

    }


    void Update()
    {
 
        float horizontal = Input.GetAxis("Horizontal") * speedRotation;
        float Vertical = Input.GetAxis("Vertical");
        float L1 = Input.GetAxis("L1"); // L1 버튼 입력시 
        if (controller.isGrounded)
        {
            Vertical *= Time.deltaTime;

            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;


        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        horizontal *= Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        if (Vertical != 0f) // 전진 키가 눌릴 경우
        {
            Player.GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Walk; // 플레이어 애니메이션 상태 걷기로
            walkSound.enabled = true;

            speed = 3f;

            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {

                Player.GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Run; // 플레이어 애니메이션 상태 뛰기로
                speed = 5f; // 속도 증가
            }
        }

        else // 아무 입력도 없을경우
        {
            Player.GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Idle; //  플레이어 애니메이션 상태 가만히
            walkSound.enabled = false;
        }


        if (Input.GetButtonDown("X")) // x키 누를시
        {
      
            if (state.actState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
            {
                state.actState = PlayerAnimState.ActionState.PhoneDown; // 폰 내리기
            }

            else if (state.actState == PlayerAnimState.ActionState.PhoneDown || state.actState == PlayerAnimState.ActionState.PhotoMode ) // 폰이 아래 있을 경우에만
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

        if (Input.GetButtonDown("B")) // b키 누를시
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

        if (Input.GetButtonDown("Y")) // y키 누를시
        {
            moveDirection.y = jumpSpeed;
        }
    }



}
