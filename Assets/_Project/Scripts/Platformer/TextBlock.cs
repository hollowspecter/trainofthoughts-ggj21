using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Yarn.Unity;

public class TextBlock : MonoBehaviour
{
	public Image background;
	public TextMeshProUGUI hoverText;
	public string setVariableTrue = "";

	private Color originalPanelColor;
	private Color originalTextColor;

	private new MeshRenderer renderer;

	bool initialised = false;
	private InMemoryVariableStorage memory;

	private void Start()
	{
		renderer = GetComponent<MeshRenderer>();
		memory = FindObjectOfType<InMemoryVariableStorage>(true);

		if (hoverText != null)
		{
			originalPanelColor = background.color;
			originalTextColor = hoverText.color;

			background.color = Color.clear;
			hoverText.color = Color.clear;

			hoverText.text = GetComponent<TextMeshPro>().text;

			gameObject.SetActive(false);
			initialised = true;
		}
	}

	private void OnEnable()
	{
		if (!initialised)
			return;

		if (hoverText != null)
		{
			background.color = originalPanelColor;
			background.DOFade(1f, 0.5f).From(0f);

			hoverText.color = originalTextColor;
			hoverText.DOFade(1f, 0.5f).From(0f);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			var text = GetComponent<TextMeshPro>().text;
			TextBlocksPanel.Instance.AddNewWord(text);
			if (string.IsNullOrWhiteSpace(setVariableTrue) == false)
			{
				memory.SetValue(setVariableTrue, true);
			}
		}
	}

	public void HideHover()
	{
		background.DOFade(0f, 0.5f);
		hoverText.DOFade(0f, 0.5f);
	}
}
