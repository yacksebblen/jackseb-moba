using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
	public LayerMask canClick;

	void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, canClick))
			{
				transform.position = hit.point + hit.normal * 0.1f;
				if (hit.collider.gameObject.layer == 10)
				{
					GetComponent<MeshRenderer>().enabled = false;
				}
				else
				{
					GetComponent<MeshRenderer>().enabled = true;
					transform.position = hit.point + hit.normal * 0.1f;
				}
			}
		}
	}
}
