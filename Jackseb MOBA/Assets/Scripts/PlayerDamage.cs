using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// hey, you already built it, make sure you can take damage from autos
// maybe work on showing the health next for local feedback and particle effects for server feedback?
// also, do some situps through out the day, that SC memories video aint doin you any favors.
// love you, keep up the good work, don't give up, you got it uwu <3 (renee - SALES) -past jack

public class PlayerDamage : MonoBehaviourPun
{
	public LayerMask canAttack;

	float timeStamp;

	PlayerMain pMain;
	PlayerMovement pMove;

	float newMaxHealth;
	public float currentHealth;

	private void Awake()
	{
		pMain = GetComponent<PlayerMain>();
		pMove = GetComponent<PlayerMovement>();
	}

	private void Start()
	{
		newMaxHealth = pMain.myChamp.maxHealth;
		currentHealth = newMaxHealth;
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

	[PunRPC]
	public void TakeDamageRPC(float dmg)
	{
		currentHealth -= dmg;
	}

	public void AutoAttack(Transform player)
	{
		timeStamp = Time.time + pMain.myChamp.autoCooldown;
		Debug.Log("Attacking " + player.name);
		player.root.gameObject.GetPhotonView().RPC("TakeDamageRPC", RpcTarget.All, pMain.GetAttackDamage());
	}
}
