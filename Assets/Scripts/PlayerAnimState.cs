using UnityEngine;
using System.Collections;

public class PlayerAnimState : MonoBehaviour
{

    public GameObject Player;
    public GameObject camera;


    public enum AnimState // 플레이어 애니메이션 상태
    {

        Idle, // 가만히 있을때
        Walk, // 걸어갈 경우
        Run, // 달려갈 경우
        Jump, // 점프할 경우


    };

    public enum ActionState // 플레이어 액션 상태
    {
        PhoneDown, // 폰내리기
        PhoneUp, // 폰올리기
        LightOn, // 라이트 켜기
        PhotoMode, // 사진 모드
        PhotoShot, // 사진 찍기
        Behind, // 숨기 
    };



    public Animation anim; // 애니메이션 객체

    [HideInInspector]
    public AnimState animState; //플레이어 현재 상태에 따른 애니메이션을 취하기 위한 변수
    public AnimState AState { get { return animState; } set { animState = value; } }  //접근권한을 위한 get set 함수

    public ActionState actState; //플레이어 현재 상태에 따른 액션을 취하기 위한 변수
    public ActionState TState { get { return actState; } set { actState = value; } }  //접근권한을 위한 get set 함수


    void Start()
    {

        animState = AnimState.Idle;
        // 첫 상태는 가만히!
        actState = ActionState.PhoneDown;
        // 첫 액션은 핸드폰 내리기

        Player = GameObject.Find("PlayerTmp");
        camera = GameObject.Find("Camera");
        anim = GetComponent<Animation>(); // 플레이어 애니메이션 컴포넌트 할당
        StartCoroutine("PlayerAnimation"); //상태에 따른 행동
        StartCoroutine("PlayerAction"); // 액션에 따른 행동

    }



    public IEnumerator PlayerAnimation()  //상태에 따른 행동
    {

        // yield return new WaitForSeconds(5f); 
        while (true)
        {
            switch (animState)
            {
                case AnimState.Idle:  // 가만히 있을경우                
                    anim.Play("tPose");
                    break;

                case AnimState.Walk: // 걸어갈 경우
                    //anim.Play("idle");
                    break;

                case AnimState.Run:// 뛰어갈 경우
                   // anim.Play("walk");
                    break;

                case AnimState.Jump:// 점프할 경우   
                    anim.Play("tPose");
                    yield return new WaitForSeconds(1.2f);
                    break;

            }

            yield return null;
        }
    }

    public IEnumerator PlayerAction()  //액션에 따른 행동
    {

        // yield return new WaitForSeconds(5f); 
        while (true)
        {
            switch (actState)
            {
                case ActionState.PhoneDown:  // 폰내릴 경우               
                    camera.transform.FindChild("RightHand").gameObject.SetActive(false);
                    camera.transform.FindChild("RightHand").transform.FindChild("smartphone").transform.FindChild("Spotlight").gameObject.SetActive(false);
                    camera.transform.FindChild("phone").gameObject.SetActive(false);
                    break;

                case ActionState.PhoneUp: // 폰 올리고 가만히
                    camera.transform.FindChild("RightHand").gameObject.SetActive(true);
                    camera.transform.FindChild("RightHand").transform.FindChild("smartphone").transform.FindChild("Spotlight").gameObject.SetActive(false);
                    camera.transform.FindChild("phone").gameObject.SetActive(false);
                    break;

                case ActionState.LightOn:// 라이트 켜기
                    camera.transform.FindChild("RightHand").transform.FindChild("smartphone").transform.FindChild("Spotlight").gameObject.SetActive(true);
                    break;

                case ActionState.PhotoMode:// 사진 모드
                    camera.transform.FindChild("phone").gameObject.SetActive(true);
                    camera.transform.FindChild("RightHand").gameObject.SetActive(false);
                    break;
                case ActionState.PhotoShot:// 사진 찍기
                    StartCoroutine("Shot");
                    break;
                case ActionState.Behind:// 숨기
                    camera.transform.FindChild("RightHand").gameObject.SetActive(false);
                    camera.transform.FindChild("RightHand").transform.FindChild("smartphone").transform.FindChild("Spotlight").gameObject.SetActive(false);
                    camera.transform.FindChild("phone").gameObject.SetActive(false);
                    break;

            }

            yield return null;
        }


    }

    public IEnumerator Shot()  //사진찍기
    {
        yield return null;
        GameObject.Find("phone").transform.FindChild("light").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("phone").transform.FindChild("light").gameObject.SetActive(false);
        actState = ActionState.PhotoMode;
    }

}
