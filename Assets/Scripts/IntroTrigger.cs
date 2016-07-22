using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Debug.Log ("change Scene");
			SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
          
		}

	}

}
