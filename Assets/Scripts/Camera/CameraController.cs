using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private float minOrtSize = 2;
	[SerializeField] private float maxOrtSize = 10;
	[SerializeField] private new Camera camera;
	
	private void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Vector3 forward = transform.forward;
		forward.y = 0;
		forward.Normalize();
		
		Vector3 right = transform.right;

		transform.position += (horizontal * right +
		                       vertical * forward) * (Time.deltaTime * speed);

		Vector2 scroll = Input.mouseScrollDelta;
		float current = camera.orthographicSize;
		float newSize = current - scroll.y;
		newSize = Mathf.Clamp(newSize, minOrtSize, maxOrtSize);
		camera.orthographicSize = newSize;
	}
}
