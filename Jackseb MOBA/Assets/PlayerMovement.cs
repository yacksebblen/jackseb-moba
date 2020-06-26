using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
	public Transform indicator;

	NavMeshAgent myAgent;

	void Start()
    {
		myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				myAgent.SetDestination(hit.point);

				indicator.position = hit.point + hit.normal * 0.1f;
			}
		}
    }
}
