using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMovement : MonoBehaviour 
{

	float horizontalSpeed = 2.0f;
	float verticalSpeed = 2.0f;

	void Update()
	{
		float h = horizontalSpeed * Input.GetAxis("Mouse X");
		float v = verticalSpeed * Input.GetAxis("Mouse Y");

		transform.Rotate(v, h, 0);
	}
}
