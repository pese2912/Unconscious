using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour
{

	[SerializeField]
	private CharacterController controller;
	[SerializeField]
	private AudioSource Audio;
	[SerializeField]
	private AudioClip walkSound;
	//[SerializeField]
	//private AudioClip runSound;
	[SerializeField]
	private float speed = 5F; // 플레이어 기본 속도
	[SerializeField]
	private float speedRotation = 60;

    [HideInInspector]
    public int evidenceCount = 0;


    private float _speedRotation;
    private float _speed;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 m_moveDirection = Vector3.zero;


    void Start()
    {
      
        _speed = speed;
        _speedRotation = speedRotation;
    }

   
    void Update()
    {
        //입력
        //getInput();
         float _rotate;
         float _horizontal;
         float _vertical;
         float m_horizontal;
         float m_vertical;
         float L1;


        _horizontal = Input.GetAxis("Horizontal"); 
        m_horizontal = Input.GetAxis("Oculus_GearVR_DpadX");
        _rotate = Input.GetAxis("Oculus_GearVR_RThumbstickX") * speedRotation;

         _vertical = Input.GetAxis("Vertical"); 
        m_vertical = Input.GetAxis("Oculus_GearVR_DpadY");
        L1 = Input.GetAxis("L1"); // L1 버튼 입력시 

        if(_vertical == 0f || _horizontal == 0f)
        {
            _speed = speed;
            PlayerAnimState.instance.isRun = false;
            Audio.enabled = false;
        }
        else if (_vertical != 0f)
        {
            //   Audio.clip = walkSound;
            if(PlayerAnimState.instance.TState != PlayerAnimState.ActionState.Behind)
              Audio.enabled = true;
            _speed = speed;

            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {
                // Audio.clip = walkSound;
                PlayerAnimState.instance.isRun = true;
                _speed += 3f; // 속도 증가
            }
        }
        else if (_horizontal != 0f)
        {
            //   Audio.clip = walkSound;
            if (PlayerAnimState.instance.TState != PlayerAnimState.ActionState.Behind)
                Audio.enabled = true;
            _speed = speed;

            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {
                // Audio.clip = walkSound;
                PlayerAnimState.instance.isRun = true;
                _speed += 3f; // 속도 증가
            }
        }
        else if (_vertical != 0f && _horizontal != 0f) // 전진 키가 눌릴 경우
        {
            //   Audio.clip = walkSound;
            if (PlayerAnimState.instance.TState != PlayerAnimState.ActionState.Behind)
                Audio.enabled = true;
            _speed = speed;

            if (L1 != 0f) // L1버튼도 같이 눌릴 경우
            {
                // Audio.clip = walkSound;
                PlayerAnimState.instance.isRun = true;
               _speed += 3f; // 속도 증가
            }
        }

        else // 아무 입력도 없을경우
        {
            _speed = speed;
            PlayerAnimState.instance.isRun = false;
            Audio.enabled = false;
        
        }


        //전진,후진
        _moveDirection = new Vector3(_horizontal, 0, _vertical);
        _moveDirection.y -= 9.8f;
        _moveDirection = transform.TransformDirection(_moveDirection);

        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection.y -= 9.8f;
        m_moveDirection = transform.TransformDirection(m_moveDirection);

        controller.Move(_moveDirection * Time.deltaTime* _speed);
        controller.Move(m_moveDirection * Time.deltaTime * _speed);

        //회전 
        _rotate *= Time.deltaTime;
        transform.Rotate(0, _rotate, 0);

        
    }


}
