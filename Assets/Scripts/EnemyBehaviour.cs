using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	private GameObject player;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);
		rb.velocity = transform.forward * 3f;
	}
}
