using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerviosPlatform : MonoBehaviour
{
	new Collider2D collider;


	private void Awake()
	{
		collider = GetComponent<Collider2D>();
	}

	public void GoThrough()
	{
		if (collider.enabled)
		{
			collider.enabled = false;
			StartCoroutine(TurnBackOn());
		}
	}

	private IEnumerator TurnBackOn()
	{
		yield return new WaitForSeconds(1f);
		collider.enabled = true;
	}
}
