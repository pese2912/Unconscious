using UnityEngine;
using System.Collections;

public class PlayerAnimState : MonoBehaviour {


    public enum AnimState // 플레이어 애니메이션 상태
    {

       Idle, // 가만히 있을때
       Walk, // 걸어갈 경우
       Run, // 달려갈 경우
       Jump, // 점프할 경우
       
    };

    public Animation anim; // 애니메이션 객체
   
    [HideInInspector]
    public AnimState animState; //플레이어 현재 상태에 따른 액션을 취하기 위한 변수
    public AnimState AState { get { return animState; } set { animState = value; } }  //접근권한을 위한 get set 함수


    void Start()
    {

        animState = AnimState.Idle; 
    
        // 첫 상태는 가만히!
        anim = GetComponent<Animation>(); // 플레이어 애니메이션 컴포넌트 할당
        StartCoroutine("PlayerAction"); //상태에 따른 행동

    }



    void Update()
    {

    

    }

    public IEnumerator PlayerAction()  //상태에 따른 행동
    {

       // yield return new WaitForSeconds(5f); 
        while (true)
        {
            switch (animState)
            {
                case AnimState.Idle:  // 가만히 있을경우                
                    anim.Play("idle");  
                    break;

                case AnimState.Walk: // 걸어갈 경우
                    anim.Play("walk");
                    break;

                case AnimState.Run:// 뛰어갈 경우
                    anim.Play("run");
                    break;

                case AnimState.Jump:// 점프할 경우   
                    anim.Play("jump");
                    yield return new WaitForSeconds(1f);
                    break;
                    
            }

            yield return null;
        }


    }

}
