using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour
{

	[SerializeField]
	private CharacterController controller;
	[SerializeField]
	private PlayerAnimState state; // 현재 플레이어 애니메이션 상태
	[SerializeField]
	private AudioSource Audio;
	[SerializeField]
	private AudioClip walkSound;
	[SerializeField]
	private AudioClip runSound;
	[SerializeField]
	private float speed = 5F; // 플레이어 기본 속도
	[SerializeField]
	private float speedRotation = 50F;

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
		//입력
		getInput();

		//전진,후진
        _moveDirection = new Vector3(_horizontal, 0, _vertical);
        _moveDirection = transform.TransformDirection(_moveDirection);
        controller.Move(_moveDirection * Time.deltaTime* _speed);

		//회전 
        _rotate *= Time.deltaTime;
        transform.Rotate(0, _rotate, 0);

        
    }

	void getInput(){
	
		_horizontal= Input.GetAxis("Horizontal");
		_rotate = -Input.GetAxis("Oculus_GearVR_RThumbstickY") * speedRotation;
		_vertical = Input.GetAxis("Vertical");
		L1 = Input.GetAxis("L1"); // L1 버튼 입력시 


		if (_vertical != 0f || _horizontal!=0f) // 전진 키가 눌릴 경우
		{
			Audio.PlayOneShot (walkSound);


			_speed = speed;

			if (L1 != 0f) // L1버튼도 같이 눌릴 경우
			{
				Audio.PlayOneShot (runSound);          
				_speed += 5f; // 속도 증가
			}
		}

		else // 아무 입력도 없을경우
		{         
			Audio.Stop ();
		}


		if (Input.GetButtonDown("R1")) // R1키 누를시
		{

			if (state.TState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
			{
				state.TState = PlayerAnimState.ActionState.PhoneDown; // 폰 내리기
			}

			else if (state.TState == PlayerAnimState.ActionState.PhoneDown || state.TState == PlayerAnimState.ActionState.PhotoMode ) // 폰이 아래 있을 경우에만
			{
				state.TState = PlayerAnimState.ActionState.PhoneUp; // 폰 올리기
			}

		}

		if (Input.GetButtonDown("X")) //X키 누를시
		{

			if (state.TState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
			{
				state.TState = PlayerAnimState.ActionState.LightOn; // 라이트 켜기
			}

			else if (state.TState == PlayerAnimState.ActionState.LightOn) // 라이트 켜있을 경우
			{
				state.TState = PlayerAnimState.ActionState.PhoneUp; // 라이트 끄기
			}
		}

		if (Input.GetButtonDown("A")) // A키 누를시
		{
			if (state.TState == PlayerAnimState.ActionState.PhoneUp) // 폰이 위로 잇을경우에만
			{
				state.TState = PlayerAnimState.ActionState.PhotoMode; // 사진 모드
			}

			else if (state.TState == PlayerAnimState.ActionState.PhotoMode) // 사진 모드일경우
			{
				state.TState = PlayerAnimState.ActionState.PhotoShot; // 사진 찍기
			}
		}

	}
		
}
