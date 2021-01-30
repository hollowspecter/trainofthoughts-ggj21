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
	private Vector2 jumpForce = new Vector2(10f, 20f);
	[SerializeField]
	private float speed = 3f;
	[SerializeField]
	private float maxFallingSpeed = 10f;

	private new Rigidbody2D rigidbody;
	private new Collider2D collider;
	private float input;
	private bool isGrounded = false;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();
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

		// push forward
		if (isGrounded)
		{
			rigidbody.velocity = velocity;
			if (input > 0f)
			{
				rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
			}
		}

		// limit falling speed
		if (rigidbody.velocity.y < -maxFallingSpeed)
		{
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallingSpeed);
		}
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
