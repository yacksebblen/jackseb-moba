using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
	public string playerPrefab;
	public Transform[] spawnPoints;

	void Start()
    {
		Spawn();
    }

    public void Spawn()
	{
		Transform mySpawn = GetSpawnPoint();
		PhotonNetwork.Instantiate(playerPrefab, mySpawn.position, mySpawn.rotation);
	}

	public Transform GetSpawnPoint()
	{
		return spawnPoints[Random.Range(0, spawnPoints.Length)];
	}
}
