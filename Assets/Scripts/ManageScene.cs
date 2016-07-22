using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ManageScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("gg");
		SceneManager.LoadScene("1.Start", LoadSceneMode.Additive);
		//SceneManager.LoadScene("intro");
	}
	

}
