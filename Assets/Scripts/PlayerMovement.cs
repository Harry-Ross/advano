using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed;

	// Use this for initialization
	private CharacterController controller;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float mouseX;
	void Start () {
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
			if (Input.GetKeyDown(KeyCode.Space)) {
				moveDirection.y += 20;
			}
		}
		moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
		if (Input.GetKey(KeyCode.LeftShift)) {
			controller.Move(moveDirection * Time.deltaTime * 1.5f);
		} else {
			controller.Move(moveDirection * Time.deltaTime);
		}

		mouseX = Input.GetAxis("Mouse X");
		
		transform.Rotate(new Vector3(0, mouseX, 0));
	}
}
