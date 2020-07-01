﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMain : MonoBehaviourPun
{
	public Behaviour[] disableRemote;

	public Champion myChamp;

	[SerializeField]
	int myLevel;

	private void Awake()
	{
		if (photonView.IsMine)
		{
			photonView.RPC("SpawnChampRPC", RpcTarget.AllBuffered, PlayerPrefs.GetString("champ"));
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
}
