using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleScreen : MonoBehaviour
{
	public GameObject blocker;
	public Image titleScreen;
	public Image blackFader;
	public GameObject skipButton;
	public float minTimeForTitleScreen = 5f;

	private bool starting = false;

	private void Awake()
	{
		blackFader.DOFade(0f, 1f).From(0f);
	}

	void Start()
	{
		if (PlayerPrefs.GetInt(SaveState.PlaythroughKey) == 1)
		{
			skipButton.SetActive(true);
		}
		else
		{
			skipButton.SetActive(false);
		}

		StartCoroutine(TitleScreenFlow());
	}

	IEnumerator TitleScreenFlow()
	{
		// wait a couple seconds
		yield return new WaitForSeconds(minTimeForTitleScreen);

		// fade it out
		yield return titleScreen.DOFade(0f, 1f).WaitForCompletion();

		// turn off blocker
		blocker.SetActive(false);
	}

	public void StartGame()
	{
		if (starting)
			return;

		starting = true;
		StartCoroutine(EStartGame());
	}

	public void SkipTutorial()
	{
		if (starting)
			return;

		starting = true;
		StartCoroutine(ESkipTutorial());
	}


	IEnumerator EStartGame()
	{
		// fade to black
		yield return blackFader.DOFade(1f, 1f).From(0f).WaitForCompletion();

		// load next level
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	IEnumerator ESkipTutorial()
	{
		// fade to black
		yield return blackFader.DOFade(1f, 1f).From(0f).WaitForCompletion();

		// load next level
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}
}
