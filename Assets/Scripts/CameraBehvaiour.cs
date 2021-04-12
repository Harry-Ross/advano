using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehvaiour : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	public float mouseY;
	public float yRotate = 0f;
	
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	void Update () {
		mouseY = Input.GetAxis("Mouse Y");
		yRotate = mouseY;
		Mathf.Clamp(yRotate, 90f, 0f);
		// if (transform.rotation.x >= 0) {
			transform.Rotate(new Vector3(mouseY, 0, 0));
		// }
		// if (transform.rotation.x < 0) {
		// 	transform.eulerAngles = new Vector3(0, transform.rotation.y, transform.rotation.z);
		// }
		
		
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
