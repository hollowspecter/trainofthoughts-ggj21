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
