using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlocksPanel : MonoBehaviour
{
	public static TextBlocksPanel Instance;

	[SerializeField]
	private GameObject textBlockPrefab;

	private void Awake()
	{
		Instance = this;
	}

	public void AddNewWord(string text)
	{
		var go = Instantiate(textBlockPrefab, transform);
		go.GetComponentInChildren<TextMeshProUGUI>().text = text;
	}
}
