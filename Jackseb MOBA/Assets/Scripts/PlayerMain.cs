using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMain : MonoBehaviourPun
{
	public Behaviour[] disableRemote;

	public Champion myChamp;

	private void Awake()
	{
		if (photonView.IsMine)
		{
			ChangeLayersRecursively(transform, 8);
		}
		else
		{
			DisableComponents();
		}
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
