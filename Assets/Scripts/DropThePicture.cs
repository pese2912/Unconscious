using UnityEngine;
using System.Collections;

public class DropThePicture : MonoBehaviour {
	public Rigidbody[] _rbs;
	public Rigidbody[] _rbsLater;
	void Start(){

		StartCoroutine(DropTheBit ()) ;

	}
	public IEnumerator DropTheBit(){
		yield return new WaitForSeconds (5.0f);
		foreach(Rigidbody rb in _rbs){
			rb.AddForce(transform.up*-1.0f * 500.0f);
			rb.AddForce(transform.right * 150.0f);
			rb.useGravity = true;
		}
		yield return new WaitForSeconds (4.0f);

		foreach(Rigidbody rb in _rbsLater){
			rb.AddForce(transform.up*-1.0f * 500.0f);
			rb.AddForce(transform.right * 150.0f);
			rb.useGravity = true;
		}
	}

}
