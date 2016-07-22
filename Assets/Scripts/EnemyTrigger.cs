using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {
	public bool isTouched=false;
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Debug.Log ("isTouch");
			if (!isTouched) {
				isTouched = true;
			} 
		}
	}
}
