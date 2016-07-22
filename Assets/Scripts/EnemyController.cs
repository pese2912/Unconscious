using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	private GameObject player;
	private NavMeshAgent nav;
	private GameObject enemyTir;
	public EnemyTrigger tri;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemyTir = GameObject.FindGameObjectWithTag ("EnemyOnTrigger");
		nav = GetComponent<NavMeshAgent> ();
		tri = enemyTir.GetComponent<EnemyTrigger> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tri.isTouched) {
			nav.SetDestination (player.transform.position);
		}
	}
}
