using UnityEngine;
using System.Collections;

public class TempPlayer : MonoBehaviour {


	public float speed = 6.0F;
	public float speedRotation=50F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public int evidenceCount = 0;
	public CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;
	public GameObject camera;
	public enum ActionState
	{
		PhoneDown, // 폰내리기
		PhoneUp, // 폰올리기
		LightOn, // 라이트 켜기
		PhotoMode, // 사진 모드
		PhotoShot, // 사진 찍기
		LightDown,//라이트 끄기

	};

	public ActionState actionState;

	public ActionState ActState{
		get{
			return actionState;
		}
		set{
			actionState = value;
		}
	}


	void Start(){
		controller = GetComponent<CharacterController>();
		camera = GameObject.Find ("Camera");
		actionState = ActionState.PhoneDown;
		StartCoroutine ("PlayCamera");
	}

	void Update() {
		float horizontal = Input.GetAxis ("Horizontal")*speedRotation;
		if (controller.isGrounded) {
			float vertical= Input.GetAxis("Vertical");
			vertical *= Time.deltaTime;

			//moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		horizontal *= Time.deltaTime;
		transform.Rotate (0, horizontal, 0);
	}

	public IEnumerator PlayCamera(){
		while (true) {
			switch (actionState) {
			case ActionState.PhoneDown:
				camera.transform.FindChild ("Hand").gameObject.SetActive (false);
				camera.transform.FindChild ("Phone").gameObject.SetActive (false);
				camera.transform.FindChild ("PictureCamera").gameObject.SetActive (false);
				camera.transform.FindChild ("Spotlight").gameObject.SetActive (false);
				break;
			case ActionState.PhoneUp:
				camera.transform.FindChild ("Hand").gameObject.SetActive (true);
				camera.transform.FindChild ("Phone").gameObject.SetActive (true);
				camera.transform.FindChild ("PictureCamera").gameObject.SetActive (false);
				camera.transform.FindChild ("Spotlight").gameObject.SetActive (false);
				break;
			case ActionState.PhotoMode:
				camera.transform.FindChild ("Hand").gameObject.SetActive (false);
				camera.transform.FindChild ("Phone").gameObject.SetActive (false);
				camera.transform.FindChild ("PictureCamera").gameObject.SetActive (true);
				camera.transform.FindChild ("Spotlight").gameObject.SetActive (false);
				break;
			case ActionState.PhotoShot:
				StartCoroutine ("Shot");
				break;
			case ActionState.LightOn:
				camera.transform.FindChild ("Spotlight").gameObject.SetActive (true);
				break;
			case ActionState.LightDown:
				camera.transform.FindChild ("Spotlight").gameObject.SetActive (false);
				break;
			}
			yield return null;
		}

	}

	public IEnumerator Shot()  //사진찍기
	{
		
		camera.transform.FindChild("Spotlight").gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		camera.transform.FindChild("Spotlight").gameObject.SetActive(false);
		actionState = ActionState.PhotoMode;
		yield return null;
	}
}