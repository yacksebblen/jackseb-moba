using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Jackseb/Ability", order = 2)]
public class Ability : ScriptableObject
{
	public string qName;
	public float qCooldown;
	public float qMana;
}
