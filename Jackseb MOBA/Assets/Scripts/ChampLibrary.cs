using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampLibrary : MonoBehaviour
{
	public Champion[] allChamps;
	public static Champion[] champs;

	private void Awake()
	{
		champs = allChamps;
	}

	public static Champion StringToChamp(string name)
	{
		foreach (Champion a in champs)
		{
			if (a.name.Equals(name)) return a;
		}

		return champs[0];
	}
}
