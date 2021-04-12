using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour {
	// Use this for initialization
	private Rigidbody rb;
	public GameObject meat;
	public Vector3 position;
	void Start () {
		rb = GetComponent<Rigidbody>();
		StartCoroutine(coroutine());
	}
	IEnumerator coroutine() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(2, 9));
			createRotation();
			for (int i = 0; i < Random.Range(100, 400); i++) {
				yield return new WaitForSeconds(0.01f);
				movement();
			}
		}
	}
	void Update() {
		position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
	}
	void movement() {
		rb.velocity = transform.right * 3f;
	}

	void createRotation() {
		transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "weapon") {
			Destroy(gameObject);
			Instantiate(meat, position, transform.rotation);
		}
	}
}
