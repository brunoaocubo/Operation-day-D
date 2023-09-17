using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsecticideDamage : MonoBehaviour
{
	[SerializeField] private float damage = 5f;

	private void OnTriggerStay(Collider other)
	{
		if(other != null)
		{
			if(other.gameObject.TryGetComponent(out Larva larva)) 
			{
				larva.TakeDamage(damage);
			}
		}
	}
}
