using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EndScreen : MonoBehaviour
{
	public Image titleScreen;
	public TextMeshProUGUI text;

	private bool exit;
	private bool restart;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(EndScreenRoutine());
	}

	IEnumerator EndScreenRoutine()
	{
		// wait a second
		yield return new WaitForSeconds(1f);

		// fade in title screen
		yield return titleScreen.DOFade(1f, 1f).From(0f).WaitForCompletion();

		// fade in text
		float value = 0f;
		while (value < 1f)
		{
			value += Time.deltaTime;
			text.color = new Color(0f, 0f, 0f, value);
			yield return null;
		}

		// wait for input
		while (!exit && !restart)
		{
			exit = Input.GetKeyDown(KeyCode.Escape);
			restart = Input.GetKeyDown(KeyCode.Return);
			yield return null;
		}

		if (exit)
		{
#if UNITY_EDITOR
			Debug.LogWarning("Quit Application now! (Doesn't work in Editor)");
#endif
			Application.Quit();
		}
		else if (restart)
		{
			// fade out text
			value = 1f;
			while (value > 0f)
			{
				value -= Time.deltaTime;
				text.color = new Color(0f, 0f, 0f, value);
				yield return null;
			}

			// fade out title
			yield return titleScreen.DOFade(0f, 1f).WaitForCompletion();

			// Restart
			SceneManager.LoadScene(0);
		}
	}
}
