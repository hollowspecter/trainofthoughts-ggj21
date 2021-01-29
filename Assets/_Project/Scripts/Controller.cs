using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	private new Rigidbody2D rigidbody;
	private float input;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		HandleInput();
	}

	private void FixedUpdate()
	{

	}

	void HandleInput()
	{
		input = Input.GetAxisRaw("Vertical");
	}
}
