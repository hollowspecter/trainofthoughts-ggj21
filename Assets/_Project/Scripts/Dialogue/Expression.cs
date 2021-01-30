using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Expression : MonoBehaviour
{
	public Emotion[] emotions;

	private Dictionary<string, Sprite> emotionLUT = new Dictionary<string, Sprite>();
	private Image image;

	private void Awake()
	{
		image = GetComponent<Image>();

		emotionLUT.Add("normal", image.sprite);

		for (int i = 0; i < emotions.Length; ++i)
		{
			emotionLUT.Add(emotions[i].name, emotions[i].sprite);
		}
	}

	public void ChangeExpressionTo(string key)
	{
		if (emotionLUT.TryGetValue(key, out Sprite sprite))
		{
			image.sprite = sprite;
		}
		else
		{
			Debug.LogWarning(key + " was not found.", this);
		}
	}

	[System.Serializable]
	public class Emotion
	{
		public string name;
		public Sprite sprite;
	}
}
