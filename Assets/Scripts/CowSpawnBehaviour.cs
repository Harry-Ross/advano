using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSpawnBehaviour : MonoBehaviour {

	public GameObject cow;
	public Vector3 position;

	private GameObject[] cows;
	public int maxCows = 20;
	private bool spawningAllowed = true;

	// Use this for initialization
	void Start () {
		position = transform.position;
		StartCoroutine(coroutine());
	}

	IEnumerator coroutine() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(5, 45));
			if (spawningAllowed) {
				float x = position.x + Random.Range(-20, 20);
				float z = position.z + Random.Range(-20, 20);
				position = new Vector3(x, position.y, z);
				Instantiate(cow, position, transform.rotation);
			}			
		}
	}
	
	// Update is called once per frame
	void Update () {
		cows = GameObject.FindGameObjectsWithTag("cow");
		if (cows.Length > maxCows) {
			spawningAllowed = false;
		} else {
			spawningAllowed = true;
		}
 	}
}
