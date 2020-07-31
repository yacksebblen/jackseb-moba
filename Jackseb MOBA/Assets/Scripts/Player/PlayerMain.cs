using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SecPlayerPrefs;

//		fix the auto attacking somehow
//		respawn player when dead
//		this includes doing the different spawns (maybe don't do teams yet)
//		maybe more feedback on autos?

public class PlayerMain : MonoBehaviourPun
{
	public Behaviour[] disableRemote;

	public Champion myChamp;

	public int myLevel;

	PlayerMovement pMove;
	PlayerDamage pDmg;

	GameManager gManager;

	private void Awake()
	{
		pMove = GetComponent<PlayerMovement>();
		pDmg = GetComponent<PlayerDamage>();

		gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		if (photonView.IsMine)
		{
			photonView.RPC("SpawnChampRPC", RpcTarget.AllBuffered, SecurePlayerPrefs.GetString("champ"));
		}
		else
		{
			DisableComponents();
		}
	}

	private void Start()
	{
		myLevel = 1;
	}

	[PunRPC]
	void SpawnChampRPC(string champString)
	{
		myChamp = ChampLibrary.StringToChamp(champString);
		Instantiate(myChamp.prefab, transform);
		if (photonView.IsMine)
		{
			ChangeLayersRecursively(transform, 8);
		}
	}

	public float GetAttackDamage()
	{
		return myChamp.attackDamage + (myChamp.autoGrowthPerLevel * (myLevel - 1));
	}

	void DisableComponents()
	{
		foreach (Behaviour comp in disableRemote)
		{
			comp.enabled = false;
		}
	}

	void ChangeLayersRecursively(Transform root, int layer)
	{
		root.gameObject.layer = layer;

		foreach (Transform child in root)
		{
			child.gameObject.layer = layer;
			ChangeLayersRecursively(child, layer);
		}
	}

	void EnableRecursively(Transform root, bool enable)
	{
		foreach (Transform child in root)
		{
			child.gameObject.SetActive(enable);
			EnableRecursively(child, enable);
		}
	}

	public void Die()
	{
		if (photonView.IsMine)
		{
			pMove.SetCameraLock(false);
		}
		
		EnableRecursively(transform, false);
		pMove.enabled = false;
		pDmg.enabled = false;

		float respawnTime = (myLevel * 2) + 8;
		StartCoroutine(RespawnWait(respawnTime));

	}

	IEnumerator RespawnWait(float time)
	{
		yield return new WaitForSeconds(time);
		Respawn();
	}

	public void Respawn()
	{
		EnableRecursively(transform, true);
		pMove.enabled = true;
		pDmg.enabled = true;

		pMove.SetDefaults();
		pDmg.SetDefaults();

		Transform mySpawn = gManager.GetSpawnPoint();
		transform.position = mySpawn.position;
		transform.rotation = mySpawn.rotation;
	}
}
