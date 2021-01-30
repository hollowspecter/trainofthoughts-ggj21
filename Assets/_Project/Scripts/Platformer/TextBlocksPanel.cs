using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlocksPanel : MonoBehaviour
{
	public static TextBlocksPanel Instance;

	[SerializeField]
	private GameObject textBlockPrefab;

	private Yarn.Unity.InMemoryVariableStorage memory;
	private List<GameObject> children = new List<GameObject>();
	private string currentAnswer;

	public string CurrentAnswer => currentAnswer;

	private void Awake()
	{
		Instance = this;
		memory = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
	}

	public void AddNewWord(string text)
	{
		var go = Instantiate(textBlockPrefab, transform);
		go.GetComponentInChildren<TextMeshProUGUI>().text = text;
		children.Add(go);
	}

	public void FetchAnswer()
	{
		// put together the answer
		string answer = "";
		var texts = GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < texts.Length; ++i)
		{
			answer += texts[i].text + " ";
		}
		Debug.Log("Chose the answer: " + answer);
		currentAnswer = answer;

		foreach (var go in children)
		{
			Destroy(go);
		}
	}
}
