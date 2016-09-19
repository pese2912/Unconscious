using UnityEngine;
using System.Collections;

public class drop : MonoBehaviour {
    private Vector3 _moveDirection = Vector3.zero;
    public CharacterController controller;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        _moveDirection = new Vector3(0, 0, 0);
        _moveDirection.y -= 9.8f;
        _moveDirection = transform.TransformDirection(_moveDirection);
        controller.Move(_moveDirection * Time.deltaTime*5f);
    }
}
