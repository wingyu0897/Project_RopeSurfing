using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Transform orientation;

	[SerializeField]
	private float moveSpeed = 5f;
	[SerializeField]
	private float jumpPower;
	[SerializeField]
	private float gravity;

    private CharacterController charController;

	private Vector3 inputVelocity;
	private Vector3 moveVelocity;
	[SerializeField]
	private float verticalMove;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		// 떠있을 때, 점프가 입력 되었을 때 중력 적용
		if (charController.isGrounded == false || verticalMove > 0)
		{
			verticalMove -= gravity * Time.fixedDeltaTime;
		}
		else
		{
			verticalMove = -0.01f;
		}

		// 최종적으로 움직일 방향 계산
		Vector3 move = new Vector3(moveVelocity.x, verticalMove, moveVelocity.z);
		// 부드러운 움직임
		Vector3 n = Vector3.Lerp(charController.velocity, move, 0.1f) * Time.fixedDeltaTime;
		n.y = verticalMove;
		charController.Move(n);
	}

	private void Update()
	{
		// 움직임 키 입력
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 moveDir = orientation.forward * vertical + orientation.right * horizontal;
		Move(moveDir);

		// 점프 키 입력
		if (Input.GetKey(KeyCode.Space) && charController.isGrounded)
		{
			verticalMove = jumpPower;
		}

		// 공중에 떠있을 때 충돌 등으로 인한 속도변화를 반영
		if (!charController.isGrounded)
		{
			moveVelocity = new Vector3(charController.velocity.x, 0, charController.velocity.z);
		}
	}

	private void Move(Vector3 direction)
	{
		// 착지했을 때와 점프키가 눌리지 않았을 때만 입력 받음
		if (charController.isGrounded && !Input.GetKey(KeyCode.Space))
		{
			inputVelocity = direction.normalized;
			moveVelocity = inputVelocity * moveSpeed;
		}

	}
}