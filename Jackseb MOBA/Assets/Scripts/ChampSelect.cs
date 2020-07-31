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

	public Button readyButton;

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

	private void Update()
	{
		readyButton.gameObject.SetActive(PhotonNetwork.CurrentRoom.PlayerCount > 0);
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
