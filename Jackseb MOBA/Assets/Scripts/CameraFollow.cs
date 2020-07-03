using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;

	private void Start()
	{
		SetTarget(GameObject.Find("Spawn").transform);
	}

	void LateUpdate()
    {
		transform.position = target.position + offset;

		transform.LookAt(target);
    }

	public void SetTarget(Transform obj)
	{
		target = obj;
	}
}
