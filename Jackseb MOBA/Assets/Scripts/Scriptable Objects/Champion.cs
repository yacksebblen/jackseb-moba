using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teacher", menuName = "Jackseb/Teacher", order = 1)]
public class Champion : ScriptableObject
{
	[Header("General")]
	public string champName;
	public GameObject prefab;

	public enum Targeting { auto, self, single, ground, skillshot };
	public enum Scope { none, circular, conic, oblong };
	public enum Effect { damage, cc, buff, debuff, dash, blink };

	[Header("NavMeshAgent")]
	public float baseOffset;
	public float speed;
	public float radius;
	public float height;

	[Header("Auto Attack")]
	public float autoRange;
	public float autoCooldown;


	[Header("Q Ability")]
	public string qName;
	public float qcooldown;
	public Targeting qTargeting;
	public Scope qScope;
	public Effect qEffect;

	[Header("W Ability")]
	public string wName;
	public float wcooldown;
	public Targeting wTargeting;
	public Scope wScope;
	public Effect wEffect;

	[Header("E Ability")]
	public string eName;
	public float ecooldown;
	public Targeting eTargeting;
	public Scope eScope;
	public Effect eEffect;

	[Header("R Ability")]
	public string rName;
	public float rcooldown;
	public Targeting rTargeting;
	public Scope rScope;
	public Effect rEffect;
}
