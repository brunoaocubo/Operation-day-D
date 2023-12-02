using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{ 
	private Animator _animator;
	private Vector3 _originalDirection;
	private void Start()
	{
		_animator = GetComponent<Animator>();
		_originalDirection = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerController>()) 
		{
			transform.LookAt(other.transform);
			_animator.SetBool("isTalking", true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<PlayerController>())
		{
			transform.LookAt(_originalDirection);
			_animator.SetBool("isTalking", false);
		}
	}
}
