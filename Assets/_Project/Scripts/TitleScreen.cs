using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleScreen : MonoBehaviour
{
	public Image titleScreen;
	public Image blackFader;
	public float minTimeForTitleScreen = 1f;
	public float maxTimeForTitleScreen = 5f;

	private bool starting = false;

	private void Awake()
	{
		blackFader.DOFade(0f, 1f).From(1f);
	}

	void Start()
	{
		StartCoroutine(TitleScreenFlow());
	}

	IEnumerator TitleScreenFlow()
	{
		bool skip = false;
		float timer = 0f;
		// wait a couple seconds
		while (timer < minTimeForTitleScreen ||
			(timer > minTimeForTitleScreen && skip) ||
			timer >= maxTimeForTitleScreen)
		{
			timer += Time.deltaTime;
			skip = Input.anyKeyDown;
			yield return null;
		}

		// fade it out
		yield return titleScreen.DOFade(0f, 1f).WaitForCompletion();
	}

	public void ContinueGame()
	{
		StartGame();
	}

	public void StartGame()
	{
		if (starting)
			return;

		starting = true;
		StartCoroutine(EStartGame(SceneManager.GetActiveScene().buildIndex + 1));
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		Debug.Log("Attempt to quit game. Does nothing in Editor");
#endif
		Application.Quit();
	}

	IEnumerator EStartGame(int index)
	{
		// fade to black
		yield return blackFader.DOFade(1f, 1f).From(0f).WaitForCompletion();

		// load next level
		SceneManager.LoadScene(index);
	}
}
