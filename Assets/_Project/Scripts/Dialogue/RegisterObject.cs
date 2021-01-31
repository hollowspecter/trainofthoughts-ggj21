using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterObject : MonoBehaviour
{
	public static Dictionary<string, GameObject> Database = new Dictionary<string, GameObject>();

	public static void ClearDatabase()
	{
		Database.Clear();
	}

	[SerializeField]
	protected bool deactivateOnStart = true;
	[SerializeField]
	protected bool fadeOutOnStart = false;

	protected bool wasDeactivated = false;

	private void Awake()
	{
		Database.Add(name, gameObject);
	}

	void Start()
	{
		if (fadeOutOnStart)
		{
			var image = GetComponent<Image>();
			image.color = new Color(1f, 1f, 1f, 0f);
		}

		if (deactivateOnStart && !wasDeactivated)
		{
			wasDeactivated = true;
			gameObject.SetActive(false);
		}
	}
}
