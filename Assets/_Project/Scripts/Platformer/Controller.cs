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
	[SerializeField]
	private Animator animator;

	[Header("Pervios Platforms")]
	[SerializeField]
	private Vector2 perviosPlatformExtraForce = new Vector2(0f, 0f);

	[Header("SpeedZones")]
	[SerializeField]
	private float speedZoneSpeed = 30f;
	[SerializeField]
	private float speedUpDamping = 0.5f;

	[Header("Sounds")]
	[FMODUnity.EventRef]
	public string jumpSound;
	[FMODUnity.EventRef]
	public string landingSound;
	[FMODUnity.EventRef]
	public string fallThroughSound;
	public FMODUnity.StudioEventEmitter slidingSound;
	public FMODUnity.StudioEventEmitter speedZoneSound;

	private new Rigidbody2D rigidbody;
	private new Collider2D collider;
	private float input;
	private bool isGrounded = false;
	private PerviosPlatform perviosPlatform;
	private bool usedJumpPad = false;
	private SlidingPlatform slidingPlatform;
	private bool inSpeedZone = false;

	private float currentSpeed;
	private bool wasGrounded;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();
		currentSpeed = speed;
	}

	void Update()
	{
		HandleInput();
	}

	private void FixedUpdate()
	{
		GroundCheck();
		HandleSpeedZone();
		HandleMovement();
		UpdateAnimator();
	}

	void UpdateAnimator()
	{
		animator.SetBool("Grounded", isGrounded);
		animator.SetBool("Sliding", slidingPlatform != null);
	}

	void HandleSpeedZone()
	{
		if (inSpeedZone)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, speedZoneSpeed, Time.deltaTime * speedUpDamping);
		}
		else
		{
			currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime * speedUpDamping);
		}
	}

	void HandleMovement()
	{
		Vector2 velocity = new Vector2(currentSpeed, rigidbody.velocity.y);

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
				animator.SetTrigger("Jump");
				rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
				FMODUnity.RuntimeManager.PlayOneShot(jumpSound);
			}

			// push through pervios platform
			else if (input < 0f && perviosPlatform != null)
			{
				FMODUnity.RuntimeManager.PlayOneShot(fallThroughSound);
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
		var hit = Physics2D.Raycast(groundCheckOrigin.position - transform.right * 0.45f, Vector2.down, 0.25f, groundLayer);
		if (hit.collider == null)
			hit = Physics2D.Raycast(groundCheckOrigin.position - transform.right * 0.25f, Vector2.down, 0.25f, groundLayer);
		if (hit.collider == null)
			hit = Physics2D.Raycast(groundCheckOrigin.position - transform.right * 0.1f, Vector2.down, 0.25f, groundLayer);
		if (hit.collider == null)
			hit = Physics2D.Raycast(groundCheckOrigin.position + transform.right * 0.1f, Vector2.down, 0.25f, groundLayer);
		if (hit.collider == null)
			hit = Physics2D.Raycast(groundCheckOrigin.position + transform.right * 0.25f, Vector2.down, 0.25f, groundLayer);
		if (hit.collider == null)
			hit = Physics2D.Raycast(groundCheckOrigin.position + transform.right * 0.45f, Vector2.down, 0.25f, groundLayer);

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

		// sliding sound start
		if (slidingPlatform != null &&
			slidingSound.IsPlaying() == false)
		{
			slidingSound.Play();
		}
		else if (slidingPlatform == null &&
			slidingSound.IsPlaying() == true)
		{
			slidingSound.Stop();
		}

		// landing sound
		if (wasGrounded == false && isGrounded == true)
		{
			FMODUnity.RuntimeManager.PlayOneShot(landingSound);
		}

		wasGrounded = isGrounded;
	}

	void HandleInput()
	{
		input = Input.GetAxisRaw("Vertical");
	}

	public void OnUseJumppad()
	{
		usedJumpPad = true;
	}

	public void ToggleSpeedZone(bool value)
	{
		inSpeedZone = value;
		if (value)
			speedZoneSound.Play();
		else
			speedZoneSound.Stop();
	}
}
