using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Inputs inputController;
	[SerializeField] private GameObject virtualCamera;

	[Header("Config Player")]
	[SerializeField] private GameObject gun;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float distanceRay = 1f;

	[SerializeField] private float rotationSpeedCamera = 5.0f;
	[SerializeField] private float rotationSmoothness = 10f;
	[SerializeField] private float minVerticalAngle =  -80f;
	[SerializeField] private float maxVerticalAngle = 80f;

	private Vector2 targetCameraRotation;
	//private Vector3 cameraRotationVelocity;
	private Rigidbody _rigidbody;
	private Vector2 _inputVector;
	private Vector2 touchStartPos;
	private Ray _ray;
	private RaycastHit _hitInfo;
	private bool _isGrounded = false;

	private void Start()
	{
		_rigidbody = GetComponentInChildren<Rigidbody>();
		targetCameraRotation = Camera.main.transform.rotation.eulerAngles;
	}

	private void Update()
	{
		_ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		Debug.DrawRay(_ray.origin, _ray.direction * distanceRay);

		RotateCamera();
		Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(targetCameraRotation), rotationSmoothness * Time.deltaTime);

		if (Input.GetKey(KeyCode.F)) 
		{
			CheckHouseID();
		}
	}

	private void FixedUpdate()
	{
		Move();
		float cameraYaw = Camera.main.transform.eulerAngles.y;
		transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
		//gun.transform.forward = Camera.main.transform.forward;
	}

	private void RotateCamera()
	{
		if (Input.touchCount > 0)
		{
			foreach(Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					if (touch.position.x > Screen.width * 0.5f)
					{
						touchStartPos = touch.position;
					}
				}

				if (touch.phase == TouchPhase.Moved)
				{
					if (touch.position.x > Screen.width * 0.25f)
					{
						Vector2 touchDelta = touch.position - touchStartPos;
						float rotationAmountX = touchDelta.x * rotationSpeedCamera * Time.deltaTime;
						float rotationAmountY = touchDelta.y * rotationSpeedCamera * Time.deltaTime;

						//Camera.main.transform.Rotate(Vector3.up, rotationAmountX, Space.World);
						//Camera.main.transform.Rotate(Vector3.left, rotationAmountY, Space.Self);

						touchStartPos = touch.position;
						targetCameraRotation.y += rotationAmountX;
						targetCameraRotation.x -= rotationAmountY;
						targetCameraRotation.x = Mathf.Clamp(targetCameraRotation.x, minVerticalAngle, maxVerticalAngle);

						touchStartPos = touch.position;
					}
				}
			}
		}
	}

	private void Move() 
	{
		_inputVector = inputController.GetMovementVector2NormalizedJoystick();
		//_inputVector = inputController.GetMovementVector2NormalizedKeyboard();


		Vector3 moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);

		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0;

		Vector3 movement = (moveDir.x * Camera.main.transform.right + moveDir.z * cameraForward).normalized;
		_rigidbody.MovePosition(_rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);	
	}

	public void CheckHouseID() 
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
