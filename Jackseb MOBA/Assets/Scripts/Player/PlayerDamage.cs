using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.AI;

public class PlayerDamage : MonoBehaviourPun
{
	public LayerMask canAttack;
	public Image healthBar;
	public Image healthBarBG;
	Quaternion intHealthRot;

	float timeStamp;

	PlayerMain pMain;
	PlayerMovement pMove;

	public float currentHealth;

	private void Awake()
	{
		pMain = GetComponent<PlayerMain>();
		pMove = GetComponent<PlayerMovement>();
	}

	private void Start()
	{
		currentHealth = pMain.myChamp.maxHealth;

		intHealthRot = healthBar.transform.rotation;
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

		if (Input.GetKeyDown(KeyCode.U))
		{
			photonView.RPC("TakeDamageRPC", RpcTarget.All, 100f);
		}
	}

	private void LateUpdate()
	{
		healthBar.transform.rotation = intHealthRot;
		healthBarBG.transform.rotation = intHealthRot;
	}

	public float GetMaxHealth()
	{
		return pMain.myChamp.maxHealth + (pMain.myChamp.healthGrowthPerLevel * (pMain.myLevel - 1));
	}

	public bool InAutoRange(Transform obj)
	{
		bool b = false;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, pMain.myChamp.autoRange, canAttack);
		foreach (Collider col in hitColliders)
		{
			b = (col.transform == obj);
		}

		Vector3 s = GetComponent<NavMeshAgent>().transform.InverseTransformDirection(GetComponent<NavMeshAgent>().velocity).normalized;
		float turn = s.x;
		if (turn > 0.1f)
		{
			b = false;
		}

		return b;
	}

	[PunRPC]
	public void TakeDamageRPC(float dmg)
	{
		currentHealth -= dmg;
		healthBar.fillAmount = (currentHealth / GetMaxHealth());
	}

	public void AutoAttack(Transform player)
	{
		timeStamp = Time.time + pMain.myChamp.autoCooldown;
		Debug.Log("Attacking " + player.name);
		if (player.root.gameObject.GetPhotonView() != null)
		{
			player.root.gameObject.GetPhotonView().RPC("TakeDamageRPC", RpcTarget.All, pMain.GetAttackDamage());
		}
	}
}
