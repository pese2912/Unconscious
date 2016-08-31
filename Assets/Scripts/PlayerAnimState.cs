using UnityEngine;
using System.Collections;

public class PlayerAnimState : MonoBehaviour
{
	[SerializeField]
	private GameObject Player;
	[SerializeField]
	private GameObject camera;
	[SerializeField]
	private GameObject RightHand;
	[SerializeField]
	private GameObject Spotlight;
	[SerializeField]
	private GameObject phone;
	[SerializeField]
	private GameObject light;



    public enum ActionState // 플레이어 액션 상태
    {
        PhoneDown, // 폰내리기
        PhoneUp, // 폰올리기
        LightOn, // 라이트 켜기
        PhotoMode, // 사진 모드
        PhotoShot, // 사진 찍기
        Behind, // 숨기

    };
		

	[SerializeField]
	private ActionState actState; //플레이어 현재 상태에 따른 액션을 취하기 위한 변수
    public ActionState TState { get { return actState; } set { actState = value; } }  //접근권한을 위한 get set 함수


    void Start()
    {

        actState = ActionState.PhoneDown;
        // 첫 액션은 핸드폰 내리기
	
        StartCoroutine("PlayerAction"); // 액션에 따른 행동

    }



    public IEnumerator PlayerAction()  //액션에 따른 행동
    {

        // yield return new WaitForSeconds(5f); 
        while (true)
        {
            switch (actState)
            {
                case ActionState.PhoneDown:  // 폰내릴 경우               
                    RightHand.SetActive(false);
                    Spotlight.SetActive(false);
                    phone.SetActive(false);
                    break;

                case ActionState.PhoneUp: // 폰 올리고 가만히
                    RightHand.SetActive(true);
                    Spotlight.SetActive(false);
                    phone.SetActive(false);
                    break;

                case ActionState.LightOn:// 라이트 켜기
                    Spotlight.SetActive(true);
                    break;

                case ActionState.PhotoMode:// 사진 모드
                    phone.SetActive(true);
                    RightHand.SetActive(false);
                    break;
                case ActionState.PhotoShot:// 사진 찍기
                    StartCoroutine("Shot");
                    break;
                case ActionState.Behind:// 숨기
                    RightHand.SetActive(false);
                    Spotlight.SetActive(false);
                    phone.SetActive(false);
                    break;

            }

            yield return null;
        }

    }

    public IEnumerator Shot()  //사진찍기
    {
        yield return null;
        light.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        light.SetActive(false);
        actState = ActionState.PhotoMode;
    }

}
