using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public GameObject plane;
    public GameObject camera;
    public float startTime;
    private bool _timerIsActive = false;

	// Use this for initialization
	void Start () {

        _timerIsActive = true;
        StartCoroutine("TimeCheck");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator TimeCheck()  //상태에 따른 행동
    {

        yield return null;
        while (true)
        {
            startTime -= Time.deltaTime;

            if (startTime < 0)
            {
                plane.active = true;
                camera.GetComponent<intro>().enabled = true;
                this.transform.gameObject.active = false;
                break;
            }
            
            yield return null;
        }
    }
}
