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
	bool frozenDuringAuto = false;

	void Awake()
	{ 
		pMain = GetComponent<PlayerMain>();
		pDmg = GetComponent<PlayerDamage>();
		myAgent = GetComponent<NavMeshAgent>();

		mainCam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
	}

	private void Start()
	{
		myAgent.baseOffset = pMain.myChamp.baseOffset;
		myAgent.speed = pMain.myChamp.speed;
		myAgent.radius = pMain.myChamp.radius;
		myAgent.height = pMain.myChamp.height;
		myAgent.updateRotation = !pMain.myChamp.instantTurning;

		SetDefaults();
	}

	public void SetDefaults()
	{
		if (photonView.IsMine)
		{
			mainCam.SetTarget(transform);
			camLocked = true;
		}
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

		if (pDmg.InAutoRange(target) || frozenDuringAuto)
		{
			StopCoroutine(AutoFreeze());
			myAgent.updatePosition = false;
			if (target != null)
			{
				myAgent.SetDestination(target.position);
			}
			frozenDuringAuto = true;
			StartCoroutine(AutoFreeze());
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

	IEnumerator AutoFreeze()
	{
		yield return new WaitForSeconds(pMain.myChamp.timeFrozenAfterAuto);
		frozenDuringAuto = false;
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

	public void SetCameraLock(bool locked)
	{
		Transform _obj;
		if (locked)
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
