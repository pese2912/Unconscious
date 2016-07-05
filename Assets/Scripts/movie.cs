using UnityEngine;
using System.Collections;

public class movie : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Handheld.PlayFullScreenMovie("vr주인공테스트.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput | FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.Fill); 
       
	}
	
	// Update is called once per frame
    void Update()
    {
    }
}
