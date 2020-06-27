using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
	public LayerMask canClick;
	NavMeshAgent myAgent;
	public Transform target;

	PlayerDamage pDmg;

	void Start()
    {
		pDmg = GetComponent<PlayerDamage>();
		myAgent = GetComponent<NavMeshAgent>();

		GameObject.Find("Main Camera").GetComponent<CameraFollow>().SetTarget(transform);
    }

    void Update()
    {
		if (target != null)
		{
			myAgent.SetDestination(target.position);
		}

		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, canClick))
			{
				if (hit.collider.gameObject.layer == 10)
				{
					target = hit.collider.transform;
				}
				else
				{
					target = null;
					myAgent.SetDestination(hit.point);
				}
			}
		}

		if (pDmg.InAutoRange(target))
		{
			myAgent.isStopped = true;
		}
		else
		{
			myAgent.isStopped = false;
		}
    }
}
