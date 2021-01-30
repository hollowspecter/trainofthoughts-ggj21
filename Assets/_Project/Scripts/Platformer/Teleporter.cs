using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public Transform teleportTo;
	[FMODUnity.EventRef]
	public string soundEvent;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.gameObject.transform.position = teleportTo.position;
			FMODUnity.RuntimeManager.PlayOneShot(soundEvent, teleportTo.position);
		}
	}
}
