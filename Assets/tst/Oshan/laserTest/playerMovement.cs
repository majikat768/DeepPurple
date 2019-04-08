using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour 
{

	public float speed = 10.0f;
	public float rotationSpeed = 100.0f;

	void Update()
	{
		float translation = Input.GetAxis("Vertical")*speed;
		float rotation = Input.GetAxis("Horizontal")*rotationSpeed;

		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);
	}
}
