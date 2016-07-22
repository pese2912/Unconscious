using UnityEngine;
using System.Collections;

public class TempPlayerState : MonoBehaviour {

	public TempPlayer actState;
	
    // Use this for initialization
	void Start () {

		actState = GetComponent<TempPlayer>();
		StartCoroutine ("PlayerKey");

	}
	public IEnumerator PlayerKey(){
		while (true) {

			if (Input.GetButtonDown ("A")) {
				Debug.Log ("Button A pressed");

				if(actState.actionState == TempPlayer.ActionState.PhoneUp){
					Debug.Log ("PhoneDown");
					actState.actionState = TempPlayer.ActionState.PhoneDown;
				}
				else//(actState.actionState == TempPlayer.ActionState.PhoneDown) {
				{
					Debug.Log ("PhoneUP");
					actState.actionState = TempPlayer.ActionState.PhoneUp;
				}
			}
			if (Input.GetButtonDown ("B")) {
				Debug.Log ("Button B pressed");

				if (actState.actionState == TempPlayer.ActionState.PhotoMode) {
					actState.actionState = TempPlayer.ActionState.PhotoShot;
				} else {
					actState.actionState = TempPlayer.ActionState.PhotoMode;
				}
			}
			if (Input.GetButtonDown ("X")) {
				Debug.Log ("Button X pressed");
				if (actState.actionState == TempPlayer.ActionState.PhoneUp) {
					actState.actionState = TempPlayer.ActionState.LightOn;
				}
				else if (actState.actionState == TempPlayer.ActionState.LightOn) {
					actState.actionState = TempPlayer.ActionState.LightDown;
				}

			}
			yield return null;
		}
	}

}
