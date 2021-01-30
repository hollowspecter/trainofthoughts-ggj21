using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
	[FMODUnity.EventRef]
	public string footstepSound;

	public void Footstep()
	{
		FMODUnity.RuntimeManager.PlayOneShot(footstepSound);
	}
}
