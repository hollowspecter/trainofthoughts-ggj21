using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (collision.TryGetComponent<Controller>(out var controller))
			{
				controller.ToggleSpeedZone(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (collision.TryGetComponent<Controller>(out var controller))
			{
				controller.ToggleSpeedZone(false);
			}
		}
	}
}
