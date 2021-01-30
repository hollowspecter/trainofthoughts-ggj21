using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
	UnityEngine.UI.Button button;

	private void Awake()
	{
		button = GetComponent<UnityEngine.UI.Button>();
	}

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			button.onClick?.Invoke();
		}
	}
}
