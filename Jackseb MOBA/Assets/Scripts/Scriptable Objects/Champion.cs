using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teacher", menuName = "Jackseb/Teacher", order = 1)]
public class Champion : ScriptableObject
{
	[Header("General")]
	public string champName;
	public GameObject prefab;

	[Header("NavMeshAgent")]
	public float baseOffset;
	public float speed;
	public float radius;
	public float height;

	[Header("Health")]
	public float maxHealth;
	public float healthGrowthPerLevel;

	[Header("Auto Attack")]
	public float attackDamage;
	public float autoGrowthPerLevel;
	public float autoRange;
	public float autoCooldown;


	[Header("Q Ability")]
	public string qName;
	public float qCooldown;
	public float qMana;

	[Header("W Ability")]
	public string wName;
	public float wCooldown;
	public float wMana;

	[Header("E Ability")]
	public string eName;
	public float eCooldown;
	public float eMana;

	[Header("R Ability")]
	public string rName;
	public float rCooldown;
	public float rMana;
}
