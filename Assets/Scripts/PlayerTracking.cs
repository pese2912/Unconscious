using UnityEngine;
using System.Collections;

public class PlayerTracking : MonoBehaviour {
    
    Transform transform;
    Transform Player;

    public float height = 0;
  public  int a = 1;

    // Use this for initialization

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>(); //플레이어 위치
        transform = GameObject.Find("Main Camera").GetComponent<Transform>(); //  카메라 위치
    }

    // Update is called once per frame

    
    void Update()
    {
       // if (transform.localPosition.y > 2) 
       // { 
       //     a =-1; 
       // }
       // else if (transform.localPosition.y < 1.7) 
       //  { 
       //     a = +1; 
       //  } 

        // transform.Translate(Vector3.up * 0.3f * Time.deltaTime * a); 


       // transform.position = Player.position + new Vector3(0f, height, 0f); //플레이어 위치를 따라감(카메라도 같이 이동)

    }

}
