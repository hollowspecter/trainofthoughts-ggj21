using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlock : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			var text = GetComponent<TextMeshPro>().text;
			TextBlocksPanel.Instance.AddNewWord(text);
		}
	}
}
