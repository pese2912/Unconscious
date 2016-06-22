using UnityEngine;
using System.Collections;

public class PlayerTest : MonoBehaviour {

    public Transform camera;
	// Use this for initialization
	void Start () {
	
	}
    public float speed = 5.0F;
    public float rotationSpeed = 100.0F;

    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed;
      //  float rotation = Input.GetAxis("Horizontal") * rotationSpeed;


        translation *= Time.deltaTime;
    //    rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
       // transform.Rotate(0, rotation, 0);

        transform.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0); //카메라 회전에 따라  플레이어도 같이 회전
    }
}
