using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
	[FMODUnity.EventRef]
	public string pauseSound;
	public FMODUnity.StudioEventEmitter pauseSnapshot;

	private bool paused = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;
			TogglePause(paused);
		}

		// Restarts the game
		if (Input.GetKeyDown(KeyCode.F1))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		// Load specific level
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SceneManager.LoadScene(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SceneManager.LoadScene(3);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SceneManager.LoadScene(4);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			SceneManager.LoadScene(5);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			SceneManager.LoadScene(6);
		}
	}

	private void TogglePause(bool isPaused)
	{
		if (isPaused)
		{
			Time.timeScale = 0f;
			FMODUnity.RuntimeManager.PlayOneShot(pauseSound);
			pauseSnapshot.Play();
		}
		else
		{
			Time.timeScale = 1f;
			FMODUnity.RuntimeManager.PlayOneShot(pauseSound);
			pauseSnapshot.Stop();
		}
	}
}
