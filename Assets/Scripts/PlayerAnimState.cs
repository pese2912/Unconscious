using UnityEngine;
using System.Collections;

public class PlayerAnimState : MonoBehaviour
{
    public static PlayerAnimState instance = null;

    public bool isRun; // 뛰고있는지 체크 (소리 발생)
    public bool isUiBtn; // ui 버튼이 떴는지 체크
    public bool isSlowOpenDoor; // 문천천히 열고 있는지 체크
    public bool isFastOpenDoor; //문 빨리열고 있는지 체크 (소리 발생)
    public bool isBehind; // 숨고 있는지 체크

    public enum ActionState // 플레이어 액션 상태
    {
        Idle, // 기본 상태
        PhoneUpLightOn, // 핸드폰 올리고 후레쉬 켜기
        PhotoModeLightOff, // 사진 모드 후레쉬 끄기
        PhotoModeLightOn, // 사진 모드 후레쉬 켜기
        PhotoShot, // 사진찍기 (후레쉬터짐 and 소리발생)
        Behind, // 숨기 
     };
		
	[SerializeField]
	private ActionState actState; //플레이어 현재 상태에 따른 액션을 취하기 위한 변수
    public ActionState TState { get { return actState; } set { actState = value; } }  //접근권한을 위한 get set 함수

    void Awake()
    {
        instance = this;
        isRun = false;
        isUiBtn = false;
        isSlowOpenDoor = false;
        isFastOpenDoor = false;
        actState = ActionState.Idle;
        // 첫 액션은 기본
    }

   

}
