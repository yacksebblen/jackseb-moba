using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using SecPlayerPrefs;

public class ChampSelect : MonoBehaviourPunCallbacks
{
	public GameObject togglePrefab;
	public ToggleGroup toggleGroup;

	private void Start()
	{
		GameObject uiGrid = GameObject.Find("Canvas/Grid");
		foreach (Champion champ in ChampLibrary.champs)
		{
			GameObject newToggle = Instantiate(togglePrefab, uiGrid.transform);
			newToggle.GetComponentInChildren<Text>().text = champ.champName;
			newToggle.GetComponent<Toggle>().onValueChanged.AddListener( (value) => { SetChamp(champ, value); } );
			newToggle.GetComponent<Toggle>().group = toggleGroup;
		}
	}

	public void SetChamp(Champion myChamp, bool pressed)
	{
		if (pressed) SecurePlayerPrefs.SetString("champ", myChamp.champName);
	}

	public void ReadyGame()
	{
		PhotonNetwork.LoadLevel(2);
	}
}
