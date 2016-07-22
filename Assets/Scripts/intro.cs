using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {


    public AudioSource audio;
    public GameObject light;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        StartCoroutine("doorSound"); 
	}
    public IEnumerator doorSound()  //상태에 따른 행동
    {

         yield return new WaitForSeconds(5f);
         audio.enabled = true;
         yield return new WaitForSeconds(2f);
         light.SetActive(true);
         yield return new WaitForSeconds(5f);
         Application.LoadLevel("Manager");         
     
    }


	// Update is called once per frame
	void Update () {
	
	}
}
