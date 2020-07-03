using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
	public LayerMask canClick;
	NavMeshAgent myAgent;
	public Transform target;

	PlayerMain pMain;
	PlayerDamage pDmg;

	void Awake()
	{ 
		pMain = GetComponent<PlayerMain>();
		pDmg = GetComponent<PlayerDamage>();
		myAgent = GetComponent<NavMeshAgent>();
    }

	private void Start()
	{
		if (photonView.IsMine)
		{
			GameObject.Find("Main Camera").GetComponent<CameraFollow>().SetTarget(transform);
		}

		myAgent.baseOffset = pMain.myChamp.baseOffset;
		myAgent.speed = pMain.myChamp.speed;
		myAgent.radius = pMain.myChamp.radius;
		myAgent.height = pMain.myChamp.height;
		myAgent.updateRotation = !pMain.myChamp.instantTurning;
	}

	void Update()
    {
		if (!photonView.IsMine) return;

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
			myAgent.updatePosition = false;
		}
		else
		{
			myAgent.updatePosition = true;
		}
	}

	private void LateUpdate()
	{
		if (myAgent.velocity.sqrMagnitude > Mathf.Epsilon && !myAgent.updateRotation)
		{
			transform.rotation = Quaternion.LookRotation(myAgent.velocity.normalized);
		}

	}
}
