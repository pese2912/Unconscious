using UnityEngine;
using System.Collections;

public class DollHead : MonoBehaviour {
	
	public Transform startPos;
	public Transform[] endPos;


	public Vector3 speed;
	public float _speed=2.0f;

	void Start () {
		
		startPos = transform;
		StartCoroutine(DropTheHead ()) ;

	}
	
	public IEnumerator DropTheHead(){
		yield return new WaitForSeconds (5f);

		int index = 0;
		while (index<endPos.Length) {
			
			startPos.position = Vector3.MoveTowards (startPos.position, endPos[index].position, _speed * Time.deltaTime);
			
			if (Vector3.Distance (startPos.position, endPos [index].position) < 1.0f) {
				index++;
			}
			transform.Rotate(speed *Time.deltaTime,Space.Self);
			yield return null;
		}
	}

}
