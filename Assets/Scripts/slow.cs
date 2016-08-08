using UnityEngine;
using System.Collections;

public class slow : MonoBehaviour {

    float currentSlowMo = 0f;
    float slowTimeAllowed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.3f;
            else
            {
                Time.timeScale = 1.0f;
            }
            //else
            //{
            //    Time.timeScale = 1.0f;
            //    Time.fixedDeltaTime = 0.02f * Time.timeScale;
            //}
        }

        //if (Time.timeScale == 0.3)
        //{
        //    currentSlowMo += Time.deltaTime;
        //}
        //if(currentSlowMo > slowTimeAllowed)
        //{
        //    currentSlowMo = 0;
        //    Time.timeScale = 1.0f;

        //}
	}
}
