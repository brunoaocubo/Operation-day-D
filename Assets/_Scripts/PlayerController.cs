﻿using Cinemachine;
using UnityEngine;
using GlobalConstants;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Inputs inputController;
	[SerializeField] private CinemachineVirtualCamera virtualCamera;

	[Header("Config Player")]
	[SerializeField] private GameObject gun;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float jumpForce;
	[SerializeField] private float distanceRay = 1f;

	private CinemachineImpulseSource _impulseSource;
	private Rigidbody _rigidbody;
	private Vector2 _inputVector;
	private Ray _ray;
	private RaycastHit _hitInfo;
	private bool _isGrounded = false;
	private string interactTagCompare = "Interactable";

	private void Start()
	{
		_rigidbody = GetComponentInChildren<Rigidbody>();
		_impulseSource = virtualCamera.GetComponent<CinemachineImpulseSource>();
	}

	private void Update()
	{
		float cameraYaw = Camera.main.transform.eulerAngles.y;
		transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
		gun.transform.rotation = Camera.main.transform.rotation;

		_ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		Debug.DrawRay(_ray.origin, _ray.direction * distanceRay);

		if (Input.GetKey(KeyCode.F)) 
		{
			InteractWithObject();
		}

		/*
		if (Input.GetMouseButtonDown(0)) 
		{
			IParticleConfig config = GetComponentInChildren<IParticleConfig>();
			config.StartParticle();
			_impulseSource.GenerateImpulse();
			gun.transform.localPosition = new Vector3(gun.transform.localPosition.x, gun.transform.localPosition.y, 0.55f);
		}
		else if (Input.GetMouseButtonUp(0)) 
		{
			IParticleConfig config = GetComponentInChildren<IParticleConfig>();
			config.StopParticle();
			gun.transform.localPosition = new Vector3(gun.transform.localPosition.x, gun.transform.localPosition.y, 0.612f);
		}*/
	}

	private void FixedUpdate()
	{
		Move();
	}

	public void JumpAction() 
	{
		if(Input.GetKey(KeyCode.Space)) 
		{
			if (_isGrounded)
			{
				_isGrounded = false;
				Vector3 jumpDirection = new Vector3(0, 1 * jumpForce, 0);
				_rigidbody.AddForce(jumpDirection, ForceMode.Impulse);
			}
		}
	}

	private void Move() 
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		_inputVector = inputController.GetMovementVector2NormalizedJoystick();
#endif
#if UNITY_EDITOR
		_inputVector = inputController.GetMovementVector2NormalizedKeyboard();
#endif

		Vector3 moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);

		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0;

		Vector3 movement = (moveDir.x * Camera.main.transform.right + moveDir.z * cameraForward).normalized;
		_rigidbody.MovePosition(_rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);	
	}

	public void InteractWithObject() 
	{
		Physics.Raycast(_ray, out _hitInfo, distanceRay);

		if (_hitInfo.collider != null) 
		{
			if (_hitInfo.collider.TryGetComponent(out HouseIdentity houseIdentity))
			{
				int houseID = houseIdentity.Id;
				houseIdentity?.PlaySceneHouse(houseID);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		/*
		if(other.gameObject.layer == Constants.LAYER_HOUSE) 
		{
			if(other.TryGetComponent(out HouseIdentity houseIdentity)) 
			{
				int houseID = houseIdentity.Id;
			}
		}*/
	}

	private void OnCollisionStay(Collision collision)
	{
		foreach(ContactPoint contactPoint in collision.contacts) 
		{
			if(Vector3.Dot(contactPoint.normal, Vector3.up) > 0) 
			{
				_isGrounded = true;
				break;
			}
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		_isGrounded = false;
	}
}
