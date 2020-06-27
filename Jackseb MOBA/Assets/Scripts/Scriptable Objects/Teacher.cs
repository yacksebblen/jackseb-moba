using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teacher", menuName = "Jackseb/Teacher", order = 1)]
public class Teacher : ScriptableObject
{
	[Header("General")]
	public string teacherName;
	public GameObject prefab;
	public float autoAttackRange;

	public enum Targeting { auto, self, single, ground, skillshot };
	public enum Scope { none, circular, conic, oblong };
	public enum Effect { damage, cc, buff, debuff, dash, blink };


	[Header("Q Ability")]
	public string qName;
	public Targeting qTargeting;
	public Scope qScope;
	public Effect qEffect;

	[Header("W Ability")]
	public string wName;
	public Targeting wTargeting;
	public Scope wScope;
	public Effect wEffect;

	[Header("E Ability")]
	public string eName;
	public Targeting eTargeting;
	public Scope eScope;
	public Effect eEffect;

	[Header("R Ability")]
	public string rName;
	public Targeting rTargeting;
	public Scope rScope;
	public Effect rEffect;
}
