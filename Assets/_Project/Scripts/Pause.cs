using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
