using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DoorRotation : MonoBehaviour
{
	[SerializeField] private Vector3 angleRotation;
	[SerializeField] private Transform doorPivot;
	[SerializeField] private AudioSource openDoor_sfx;
	
	public void RotationWithSmooth() 
	{
		if (doorPivot.rotation.x == angleRotation.x && 
			doorPivot.rotation.y == angleRotation.y &&
			doorPivot.rotation.z == angleRotation.z)
		{
			return;
		}

		doorPivot.DORotate(angleRotation, 1.5f, RotateMode.Fast);
		openDoor_sfx.Play();
	}
	
	public void Rotation() 
	{
		if (doorPivot.rotation.x == angleRotation.x &&
		doorPivot.rotation.y == angleRotation.y &&
	    doorPivot.rotation.z == angleRotation.z)
		{
			return;
		}

		doorPivot.transform.Rotate(angleRotation);
		openDoor_sfx.Play();
	}
}
