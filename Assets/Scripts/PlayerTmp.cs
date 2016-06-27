using UnityEngine;
using System.Collections;

public class PlayerTmp : MonoBehaviour {

    public Transform Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Player.transform.position;
       // transform.eulerAngles = new Vector3(0, Player.transform.eulerAngles.y, 0);
      //  print(transform.eulerAngles);
	}
}
