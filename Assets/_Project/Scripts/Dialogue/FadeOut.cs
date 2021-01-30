using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FadeOut : MonoBehaviour
{
	public float fadeoutTime = 5f;
	TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		StartCoroutine(FadingOut());
	}

	private IEnumerator FadingOut()
	{
		yield return new WaitForSeconds(fadeoutTime);
		float value = 1f;
		while (value > 0f)
		{
			value -= Time.deltaTime;
			text.alpha = value;
			yield return null;
		}
		text.alpha = 0f;
	}
}
