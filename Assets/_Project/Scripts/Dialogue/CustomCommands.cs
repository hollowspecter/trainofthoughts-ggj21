using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;
using Cinemachine;

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
		dialogueRunner.AddCommandHandler("cam", ChangeCameraPrio);
		dialogueRunner.AddCommandHandler("add_to_answer", AddWordsToTextBlock);
		dialogueRunner.AddCommandHandler("end", EndScene);
		dialogueRunner.AddCommandHandler("make", ChangeExpression);
	}

	// used like <<make Phoebe angry>>
	private void ChangeExpression(string[] parameters)
	{
		// Go through each parameter and use it to find an object
		if (RegisterObject.Database.TryGetValue(parameters[0], out var person))
		{
			var expression = person.GetComponent<Expression>();
			expression.ChangeExpressionTo(parameters[1]);
		}
		else
		{
			Debug.LogError($"Was unable to find \"{parameters[0]}\", make sure you've attached a RegisterObject component!");
		}
	}

	private void EndScene(string[] parameters)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void AddWordsToTextBlock(string[] parameters)
	{
		string answer = "";
		for (int i = 0; i < parameters.Length; ++i)
		{
			answer += parameters[i] + " ";
		}
		TextBlocksPanel.Instance.AddNewWord(answer);
	}

	private void ChangeCameraPrio(string[] parameters)
	{
		// Go through each parameter and use it to find an object
		if (RegisterObject.Database.TryGetValue(parameters[0], out var objectToFade))
		{
			var camera = objectToFade.GetComponent<CinemachineVirtualCamera>();
			camera.Priority = int.Parse(parameters[1]);
		}
		else
		{
			Debug.LogError($"Was unable to find \"{parameters[0]}\", make sure you've attached a RegisterObject component!");
		}
	}

	private void FadeInSprite(string[] parameters)
	{
		// Go through each parameter and use it to find an object
		for (int i = 0; i < parameters.Length; ++i)
		{
			if (RegisterObject.Database.TryGetValue(parameters[i], out var objectToFade))
			{
				if (objectToFade != null)
				{
					var image = objectToFade.GetComponent<Image>();
					image.DOFade(1f, 1f);
				}
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
				if (objectToFade != null)
				{
					var image = objectToFade.GetComponent<Image>();
					image.DOFade(0f, 1f);
				}
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
