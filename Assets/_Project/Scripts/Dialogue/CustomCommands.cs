﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;

[RequireComponent(typeof(DialogueRunner))]
public class CustomCommands : MonoBehaviour
{
	private DialogueRunner dialogueRunner;

	private void Awake()
	{
		dialogueRunner = GetComponent<DialogueRunner>();

		dialogueRunner.AddCommandHandler("activate", ActivateGameObject);
		dialogueRunner.AddCommandHandler("deactivate", DeactivateGameObject);
		dialogueRunner.AddCommandHandler("fadein", FadeInSprite);
		dialogueRunner.AddCommandHandler("fadeout", FadeOutSprite);
	}

	private void FadeInSprite(string[] parameters)
	{
		// Go through each parameter and use it to find an object
		for (int i = 0; i < parameters.Length; ++i)
		{
			if (RegisterObject.Database.TryGetValue(parameters[i], out var objectToFade))
			{
				var image = objectToFade.GetComponent<Image>();
				image.DOFade(1f, 1f);
			}
			else
			{
				Debug.LogError($"Was unable to find \"{parameters[i]}\", make sure you've attached a RegisterObject component!");
			}
		}
	}

	private void FadeOutSprite(string[] parameters)
	{
		// Go through each parameter and use it to find an object
		for (int i = 0; i < parameters.Length; ++i)
		{
			if (RegisterObject.Database.TryGetValue(parameters[i], out var objectToFade))
			{
				var image = objectToFade.GetComponent<Image>();
				image.DOFade(0f, 1f);
			}
			else
			{
				Debug.LogError($"Was unable to find \"{parameters[i]}\", make sure you've attached a RegisterObject component!");
			}
		}
	}

	private void ActivateGameObject(string[] parameters)
	{
		ToggleGameObject(true, parameters);
	}

	private void DeactivateGameObject(string[] parameters)
	{
		ToggleGameObject(false, parameters);
	}

	private void ToggleGameObject(bool active, string[] parameters)
	{
		// Go through each parameter and use it to find an object
		for (int i = 0; i < parameters.Length; ++i)
		{
			if (RegisterObject.Database.TryGetValue(parameters[i], out var objectToActivate))
			{
				objectToActivate.SetActive(active);
			}
			else
			{
				Debug.LogError($"Was unable to find \"{parameters[i]}\", make sure you've attached a RegisterObject component!");
			}
		}
	}
}
