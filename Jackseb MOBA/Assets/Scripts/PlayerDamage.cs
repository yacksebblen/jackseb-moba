using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
	public LayerMask canAttack;

	float timeStamp;

	PlayerMain pMain;
	PlayerMovement pMove;

	private void Awake()
	{
		pMain = GetComponent<PlayerMain>();
		pMove = GetComponent<PlayerMovement>();
	}

    void Update()
    {
		if (InAutoRange(pMove.target))
		{
			if (timeStamp <= Time.time)
			{
				AutoAttack(pMove.target.transform);
			}
		}
	}

	public bool InAutoRange(Transform obj)
	{
		bool b = false;
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, pMain.myChamp.autoRange, canAttack);
		foreach (Collider col in hitColliders)
		{
			b = (col.transform == obj);
		}

		return b;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, GetComponent<PlayerMain>().myChamp.autoRange);
	}

	public void TakeDamage()
	{

	}

	public void AutoAttack(Transform player)
	{
		timeStamp = Time.time + pMain.myChamp.autoCooldown;
		Debug.Log("Attacking " + player.name);
	}
}
