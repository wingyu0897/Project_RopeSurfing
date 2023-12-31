using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField]
	private float sensX;
	[SerializeField]
	private float sensY;

	[SerializeField]
	private Transform orientation;

	float xRotation;
	float yRotation;

	private void Start()
	{
		// 커서를 안보이게 하고 중앙에 고정시킨다.
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}

	private void Update()
	{
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		yRotation += mouseX;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
	}
}
