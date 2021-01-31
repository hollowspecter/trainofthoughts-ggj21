using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
	public UnityEvent onEnter;
	public UnityEvent onExit;
	public bool disableRendererOnStart = true;

	private void Awake()
	{
		if (disableRendererOnStart &&
			TryGetComponent<Renderer>(out var renderer))
		{
			renderer.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			onEnter?.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			onExit?.Invoke();
		}
	}
}
