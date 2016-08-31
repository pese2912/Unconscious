using UnityEngine;
using System.Collections;

public class Behind : MonoBehaviour
{


    private Transform _player;
    private float _distance;
    private PlayerAnimState _state;
    private CharacterController _controller;
    public float distance;
    public GameObject move;



    // Use this for initialization
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>(); //플레이어 객체
        _state = _player.GetComponent<PlayerAnimState>();// 플레이어 상태
        _controller = _player.GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {

        _distance = Vector3.Distance(transform.position, _player.position); // 물건과 플레이어간의 거리차이

        if (_distance <= distance && Input.GetButtonDown("Jump")) // 일정한 거리내에 R1키를 눌렀을시
        {

			if (_state.TState != PlayerAnimState.ActionState.Behind)// 숨기 상태가 아니면
            {

				_state.TState = PlayerAnimState.ActionState.Behind; // 상태를 숨기로
                _player.localScale = new Vector3(0.1f, 0.1f, 0.1f); // 크기를 작게
                _controller.enabled = false;  // 컨트롤러 비활성
                move.active = true; // 무브!

            }

            else // 숨기 상태이면
            {
				_state.TState = PlayerAnimState.ActionState.PhoneDown; //기본으로
                _player.localScale = new Vector3(1f, 1f, 1f); //원래 크기로
                _controller.enabled = true; // 컨트롤러 활성
                move.active = false; 
            }
        }
    }
}
