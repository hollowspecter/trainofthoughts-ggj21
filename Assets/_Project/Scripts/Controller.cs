using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField]
	private Transform groundCheckOrigin;
	[SerializeField]
	private LayerMask groundLayer;
	[SerializeField]
	private float jumpForce = 3f;
	[SerializeField]
	private float speed = 3f;

	private new Rigidbody2D rigidbody;
	private float input;
	private bool isGrounded = false;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		HandleInput();
	}

	private void FixedUpdate()
	{
		GroundCheck();
		HandleMovement();
	}

	void HandleMovement()
	{
		Vector2 velocity = new Vector2(speed, rigidbody.velocity.y);
	}

	void GroundCheck()
	{
		var hit = Physics2D.Raycast(groundCheckOrigin.position, Vector2.down, 0.2f, groundLayer);
		if (hit.collider != null)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
	}

	void HandleInput()
	{
		input = Input.GetAxisRaw("Vertical");
	}
}
