using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[Header("General")]
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

	[Header("Pervios Platforms")]
	[SerializeField]
	private Vector2 perviosPlatformExtraForce = new Vector2(0f, 0f);

	private new Rigidbody2D rigidbody;
	private new Collider2D collider;
	private float input;
	private bool isGrounded = false;
	private PerviosPlatform perviosPlatform;
	private bool usedJumpPad = false;
	private SlidingPlatform slidingPlatform;

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

		if (isGrounded)
		{
			// push forward
			if (!usedJumpPad)
			{
				rigidbody.velocity = velocity;
			}

			// jump up only when not sliding
			if (input > 0f && slidingPlatform == null)
			{
				rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
			}

			// push through pervios platform
			else if (input < 0f && perviosPlatform != null)
			{
				perviosPlatform.GoThrough();
				rigidbody.AddForce(perviosPlatformExtraForce, ForceMode2D.Impulse);
			}
		}
		else
		{
			usedJumpPad = false;
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
			perviosPlatform = hit.collider.GetComponent<PerviosPlatform>();
			slidingPlatform = hit.collider.GetComponent<SlidingPlatform>();
		}
		else
		{
			isGrounded = false;
			perviosPlatform = null;
			slidingPlatform = null;
		}
	}

	void HandleInput()
	{
		input = Input.GetAxisRaw("Vertical");
	}

	public void OnUseJumppad()
	{
		usedJumpPad = true;
	}
}
