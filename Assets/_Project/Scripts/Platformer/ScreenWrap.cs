using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
	public Transform Top;
	public Transform Bottom;

	bool isWrappingY = false;

	private void Update()
	{
		CheckScreenWrap();
	}

	bool CheckBoundsIfVisible()
	{
		if (transform.position.y > Top.position.y ||
			transform.position.y < Bottom.position.y)
			return false;
		return true;
	}

	void CheckScreenWrap()
	{
		var isVisible = CheckBoundsIfVisible();

		if (isVisible)
		{
			isWrappingY = false;
			return;
		}

		if (isWrappingY)
		{
			return;
		}

		var newPosition = transform.position;

		if (!isWrappingY)
		{
			if (transform.position.y > Top.position.y)
			{
				isWrappingY = true;
				newPosition.y = Bottom.position.y;
			}
			else if (transform.position.y < Bottom.position.y)
			{
				isWrappingY = true;
				newPosition.y = Top.position.y;
			}

			transform.position = newPosition;
		}
	}
}
