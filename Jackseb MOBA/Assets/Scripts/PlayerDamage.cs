using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
	public LayerMask canAttack;
	public Teacher currentTeacher;

	void Start()
    {
        
    }

    void Update()
    {
		
	}

	private void OnDrawGizmos()
	{
		if (currentTeacher != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, currentTeacher.autoAttackRange);
		}
	}
}
