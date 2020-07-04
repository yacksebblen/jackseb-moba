using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;

	public float edgeSize = 30f;
	public float moveAmount = 50f;

	void LateUpdate()
    {
		if (target != null)
		{
			OneTickPosition(target);
		}
		else if (!Input.GetKeyDown(KeyCode.Space))
		{
			if (Input.mousePosition.x > Screen.width - edgeSize)
			{
				transform.position += new Vector3(moveAmount, 0, 0) * Time.deltaTime;
			}
			if (Input.mousePosition.x < edgeSize)
			{
				transform.position -= new Vector3(moveAmount, 0, 0) * Time.deltaTime;
			}
			if (Input.mousePosition.y > Screen.height - edgeSize)
			{
				transform.position += new Vector3(0, 0, moveAmount) * Time.deltaTime;
			}
			if (Input.mousePosition.y < edgeSize)
			{
				transform.position -= new Vector3(0, 0, moveAmount) * Time.deltaTime;
			}
		}
    }

	public void OneTickPosition(Transform _target)
	{
		transform.position = _target.position + offset;

		transform.LookAt(_target);
	}

	public void SetTarget(Transform obj)
	{
		target = obj;
	}
}
