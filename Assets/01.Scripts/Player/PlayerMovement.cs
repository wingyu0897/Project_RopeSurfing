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
		// ������ ��, ������ �Է� �Ǿ��� �� �߷� ����
		if (charController.isGrounded == false || verticalMove > 0)
		{
			verticalMove -= gravity * Time.fixedDeltaTime;
		}
		else
		{
			verticalMove = -0.01f;
		}

		// ���������� ������ ���� ���
		Vector3 move = new Vector3(moveVelocity.x, verticalMove, moveVelocity.z);
		// �ε巯�� ������
		Vector3 n = Vector3.Lerp(charController.velocity, move, 0.1f) * Time.fixedDeltaTime;
		n.y = verticalMove;
		charController.Move(n);
	}

	private void Update()
	{
		// ������ Ű �Է�
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 moveDir = orientation.forward * vertical + orientation.right * horizontal;
		Move(moveDir);

		// ���� Ű �Է�
		if (Input.GetKey(KeyCode.Space) && charController.isGrounded)
		{
			verticalMove = jumpPower;
		}

		// ���߿� ������ �� �浹 ������ ���� �ӵ���ȭ�� �ݿ�
		if (!charController.isGrounded)
		{
			moveVelocity = new Vector3(charController.velocity.x, 0, charController.velocity.z);
		}
	}

	private void Move(Vector3 direction)
	{
		// �������� ���� ����Ű�� ������ �ʾ��� ���� �Է� ����
		if (charController.isGrounded && !Input.GetKey(KeyCode.Space))
		{
			inputVelocity = direction.normalized;
			moveVelocity = inputVelocity * moveSpeed;
		}

	}
}