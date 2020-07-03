using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Champion", menuName = "Jackseb/Champion", order = 1)]
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
	public bool instantTurning = true;

	[Header("Health")]
	public float maxHealth;
	public float healthGrowthPerLevel;

	[Header("Auto Attack")]
	public float attackDamage;
	public float autoGrowthPerLevel;
	public float autoRange;
	public float autoCooldown;

	[Header("Abilities")]
	public Ability q;
	public Ability w;
	public Ability e;
	public Ability r;
}
