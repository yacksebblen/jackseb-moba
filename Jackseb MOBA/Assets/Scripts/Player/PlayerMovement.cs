using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Runtime;

public class PlayerMovement : MonoBehaviourPun
{
	public LayerMask canClick;
	NavMeshAgent myAgent;
	public Transform target;

	PlayerMain pMain;
	PlayerDamage pDmg;

	CameraFollow mainCam;

	bool camLocked;

	void Awake()
	{ 
		pMain = GetComponent<PlayerMain>();
		pDmg = GetComponent<PlayerDamage>();
		myAgent = GetComponent<NavMeshAgent>();

		mainCam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
	}

	private void Start()
	{
		if (photonView.IsMine)
		{
			mainCam.SetTarget(transform);
			camLocked = true;
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
			myAgent.SetDestination(target.position);
		}
		else
		{
			myAgent.nextPosition = transform.position;
			myAgent.updatePosition = true;
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			ToggleCameraLock();
		}
		else if (Input.GetKey(KeyCode.Space))
		{
			mainCam.OneTickPosition(transform);
		}
	}

	private void LateUpdate()
	{
		if (myAgent.velocity.sqrMagnitude > Mathf.Epsilon && !myAgent.updateRotation)
		{
			transform.rotation = Quaternion.LookRotation(myAgent.velocity.normalized);
		}

	}

	void ToggleCameraLock()
	{
		camLocked = !camLocked;

		Transform _obj;
		if (camLocked)
		{
			_obj = transform;
		}
		else
		{
			_obj = null;
		}

		mainCam.SetTarget(_obj);
	}
}
