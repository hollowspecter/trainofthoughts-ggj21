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
		text.color = Color.black;
	}

	private void Start()
	{
		StartCoroutine(FadingOut());
	}

	private IEnumerator FadingOut()
	{
		float value = 0f;
		while (value < 1f)
		{
			value += Time.deltaTime;
			text.color = new Color(value, value, value);
			yield return null;
		}
		yield return new WaitForSeconds(fadeoutTime - 1f);
		value = 1f;
		while (value > 0f)
		{
			value -= Time.deltaTime;
			text.color = new Color(value, value, value, value);
			yield return null;
		}
		text.color = Color.clear;
	}
}
