using UnityEngine;
using System.Collections;

public class VideoPlay : MonoBehaviour {
	


	// Use this for initialization
	void Start () {	


		Handheld.PlayFullScreenMovie ("3.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
	}
	
	// Update is called once per frame
//	void Update () {
	
//	}
}
