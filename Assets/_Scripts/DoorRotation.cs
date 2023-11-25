using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DoorRotation : MonoBehaviour
{
	[SerializeField] private Vector3 angleRotation;
	[SerializeField] private Transform doorPivot;
	[SerializeField] private AudioSource openDoor_sfx;
	
	public IEnumerator Rotation() 
	{
		if (doorPivot.rotation.y == angleRotation.y)
		{
			yield break;
		}

		doorPivot.DORotate(angleRotation, 1.5f, RotateMode.Fast);
		openDoor_sfx.Play();
		yield return new WaitForEndOfFrame();
	}
}
