using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;
	
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private Transform orientation;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = orientation.forward * vertical + orientation.right * horizontal;
		charController.Move(moveDir.normalized * Time.deltaTime * moveSpeed);
	}
}
