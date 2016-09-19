using UnityEngine;
using System.Collections;

public class ChangeSmallSofa : MonoBehaviour {
	public GameObject blood;
	// Use this for initialization
	void Start () {
		
		transform.Translate (Vector3.up * 3.0f);
		//transform.position = new Vector3 (-1.8f, 0.5f, 4.3f);
		transform.rotation = Quaternion.Euler(7.5f, 76f, -153f);
		blood.SetActive (true);
	}
	

}
