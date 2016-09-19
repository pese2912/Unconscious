using UnityEngine;
using System.Collections;

public class MonsterSiteCtrl : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		//Debug.Log (col.tag);
		if (col.CompareTag ("DOOR2")) {
			
			col.gameObject.GetComponent<OpenDoor> ().MonsterOpenDoor();
		}
			
	}
}
