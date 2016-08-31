using UnityEngine;
using System.Collections;

public class EvidenceEvent : MonoBehaviour {
    public PlayerAnimState tmpPlayer;
	public bool isTouched;
	public Animator ani;
	// Use this for initialization
	void Start () {
		
		tmpPlayer = GameObject.Find ("Player").GetComponent<PlayerAnimState> ();
		isTouched = false;
		ani = transform.FindChild ("Skeleton").GetComponent<Animator> ();
	}
	
	public void OnTriggerStay(Collider col){
		Debug.Log ("toched");
		if (tmpPlayer.TState==PlayerAnimState.ActionState.PhotoShot && !isTouched)
        {
			Debug.Log ("Evidence");
			//isTouched = true;
			//tmpPlayer.evidenceCount++;
			StartCoroutine ("StartShow");

		}
	}

	public IEnumerator StartShow(){
		yield return null;
		transform.FindChild ("Skeleton").gameObject.SetActive (true);
		//yield return new WaitForSeconds (2F);
		if (ani) {
			ani.SetBool ("Find", true);
		}
		yield return new WaitForSeconds (3F);
		if (ani) {
			ani.SetBool ("Find", false);
			ani.SetBool ("Finish", true);
		}
		yield return new WaitForSeconds (5F);
		if (ani) {
			ani.SetBool ("Finish", false);
		}
		transform.FindChild ("Skeleton").gameObject.SetActive (false);
	}
}
