using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BreakingPlatform : MonoBehaviour
{
	[SerializeField]
	float timeUntilBreak;
	[SerializeField]
	Transform shakable;
	[FMODUnity.EventRef]
	public string soundEvent;

	public UnityEvent onBreak;

	bool isBreaking = false;
	new Rigidbody2D rigidbody2D;
	new Collider2D collider;

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.isKinematic = true;
		collider = GetComponent<Collider2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isBreaking && collision.collider.CompareTag("Player"))
		{
			isBreaking = true;
			StartCoroutine(Break());
		}
	}

	private IEnumerator Break()
	{
		shakable.DOShakeRotation(timeUntilBreak, 20f, 10, 90, false);
		yield return new WaitForSeconds(timeUntilBreak);
		collider.enabled = false;
		onBreak?.Invoke();
		FMODUnity.RuntimeManager.PlayOneShot(soundEvent, transform.position);
		shakable.gameObject.SetActive(false);
		yield return new WaitForSeconds(2f);
		shakable.gameObject.SetActive(true);
		collider.enabled = true;
		isBreaking = false;
	}
}
