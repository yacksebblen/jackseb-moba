using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using SecPlayerPrefs;

public class ChampSelect : MonoBehaviourPunCallbacks
{
	public GameObject buttonPrefab;

	private void Start()
	{
		GameObject uiGrid = GameObject.Find("Canvas/Grid");
		foreach (Champion champ in ChampLibrary.champs)
		{
			GameObject newButton = Instantiate(buttonPrefab, uiGrid.transform);
			newButton.GetComponentInChildren<Text>().text = champ.champName;
			newButton.GetComponent<Button>().onClick.AddListener(() => { SetChamp(champ); });
		}
	}

	public void SetChamp(Champion myChamp)
	{
		SecurePlayerPrefs.SetString("champ", myChamp.champName);
	}

	public void ReadyGame()
	{
		PhotonNetwork.LoadLevel(2);
	}
}
