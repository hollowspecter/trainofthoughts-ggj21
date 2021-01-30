using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
	public float jumpForce = 20f;

	new Collider2D collider;

	private void Awake()
	{
		collider = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (collision.TryGetComponent<Rigidbody2D>(out var rigidbody))
			{
				rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
				collider.enabled = false;
				rigidbody.GetComponent<Controller>().OnUseJumppad();
				StartCoroutine(TurnBackOn());
			}
		}
	}


	private IEnumerator TurnBackOn()
	{
		yield return new WaitForSeconds(1f);
		collider.enabled = true;
	}
}
