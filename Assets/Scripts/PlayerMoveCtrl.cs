using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour
{

    public Transform camera; // 메인카메라 객체
    public GameObject Player;
    public CharacterController controller;
    public PlayerAnimState state; // 현재 플레이어 애니메이션 상태
    public AudioSource walkSound;
    public float speed = 1F; // 플레이어 기본 속도
    public float speedRotation = 50F;

    [HideInInspector]
    public int evidenceCount = 0;

    private float _speedRotation;
    private float _speed;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotate;
    private float _horizontal;
    private float _vertical;
    private float L1;

    void Start()
    {
      
        _speed = speed;
        _speedRotation = speedRotation;
    }


    void Update()
    {
        _horizontal= Input.GetAxis("Horizontal");
        _rotate = -Input.GetAxis("Oculus_GearVR_RThumbstickY") * speedRotation;
        _vertical = Input.GetAxis("Vertical");
        L1 = Input.GetAxis("L1"); // L1 버튼 입력시 

        _moveDirection = new Vector3(_horizontal, 0, _vertical);
        _moveDirection = transform.TransformDirection(_moveDirection);
        controller.Move(_moveDirection * Time.deltaTime* _speed);


        _rotate *= Time.deltaTime;
        transform.Rotate(0, _rotate, 0);

        if (_vertical != 0f || _horizontal!=0f) // 전진 키가 눌릴 경우
        {
            Player.GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Walk; // 플레이어 애니메이션 상태 걷기로
            walkSound.enabled = true;

            _speed = speed;

            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {

                Player.GetComponent<PlayerAnimState>().animState = PlayerAnimState.AnimState.Run; // 플레이어 애니메이션 상태 뛰기로
                _speed += 5f; // 속도 증가
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
         
        }
 
    }



}
