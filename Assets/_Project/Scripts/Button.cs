﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
	public UnityEvent onButtonPress;

	public bool pushDownToPress;

	bool inTrigger = false;
	bool pushed = false;

	private void Update()
	{
		if (inTrigger)
		{
			if (Input.GetAxisRaw("Vertical") < 0f)
			{
				PushButton();
			}
		}
	}

	public void PushButton()
	{
		if (!pushed)
		{
			Debug.Log("Pushed " + gameObject.name);
			onButtonPress?.Invoke();
			pushed = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			inTrigger = true;
			if (pushDownToPress == false)
			{
				PushButton();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			inTrigger = false;
		}
	}
}
