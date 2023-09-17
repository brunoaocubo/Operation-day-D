using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsecticideAttackSize : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other != null)
		{
			if(other.gameObject.TryGetComponent(out Larva larva)) 
			{
				larva.TakeDamage(5);
			}
		}
	}
}
