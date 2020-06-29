using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChampSelect : MonoBehaviourPunCallbacks
{


	public void SetChamp(Champion myChamp)
	{
		PlayerPrefs.SetString("champ", myChamp.champName);
	}

	public void ReadyGame()
	{
		PhotonNetwork.LoadLevel(2);
	}
}
