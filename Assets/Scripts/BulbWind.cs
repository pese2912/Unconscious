using UnityEngine;
using System.Collections;

public class BulbWind : MonoBehaviour {
	
	Vector3 _speed = new Vector3(20.0f,0.0f,0.0f);
	public bool isDown=false;
	void Start()
	{
		StartCoroutine(WindBulb());
	}
	IEnumerator WindBulb(){
		while (true) {
			if (transform.rotation.x >= 0.1f ) {
				isDown = !isDown;
			} 
			else if (transform.rotation.x <= -0.1f ) {
				isDown = !isDown;
			} 

			if (isDown) {
				transform.Rotate (_speed * -1.0f * Time.deltaTime);
			} else {
				transform.Rotate (_speed * Time.deltaTime);
			}
			yield return new WaitForSeconds (0.01f);
		}


	}
	

}
