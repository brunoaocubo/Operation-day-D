using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Config Player")]
	[SerializeField] InputController inputController;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;

	private Vector2 _inputVector;
	private Rigidbody _rigidbody;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_rigidbody = GetComponentInChildren<Rigidbody>();
	}

	private void OnEnable()
	{
		inputController.OnJumpAction += InputController_OnJumpAction;
		inputController.OnCrouchAction += InputController_OnCrouchAction;
		inputController.OnInteractAction += InputController_OnInteractAction;
	}

	private void OnDisable()
	{
		inputController.OnJumpAction -= InputController_OnJumpAction;
		inputController.OnCrouchAction -= InputController_OnCrouchAction;
		inputController.OnInteractAction -= InputController_OnInteractAction;
	}

	private void Update()
	{
		float cameraYaw = Camera.main.transform.eulerAngles.y;
		transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void InputController_OnJumpAction(object sender, System.EventArgs e)
	{

		Debug.Log("Pulando");
	}

	private void InputController_OnCrouchAction(object sender, System.EventArgs e)
	{
		Debug.Log("Agachando");
	}

	private void InputController_OnInteractAction(object sender, System.EventArgs e)
	{
		Debug.Log("Interagindo");
	}


	private void Move() 
	{
		_inputVector = inputController.GetMovementVector2Normalized();
		Vector3 moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);
		moveDir = Camera.main.transform.rotation * moveDir;
		_rigidbody.MovePosition(_rigidbody.position + moveDir * moveSpeed * Time.fixedDeltaTime);	
	}
}
