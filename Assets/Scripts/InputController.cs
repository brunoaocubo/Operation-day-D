using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
	private PlayerInput _playerActionsMap;
	public event EventHandler OnJumpAction;
	public event EventHandler OnCrouchAction;
	public event EventHandler OnInteractAction;

	private void Awake()
	{
		_playerActionsMap = new PlayerInput();
	}

	private void OnEnable()
	{
		_playerActionsMap.Enable();
		_playerActionsMap.PlayerAction.Jump.performed += Jump_performed;
		_playerActionsMap.PlayerAction.Crouch.performed += Crouch_performed;
		_playerActionsMap.PlayerAction.Interact.performed += Interact_performed;
	}

	private void Jump_performed(InputAction.CallbackContext obj)
	{
		OnJumpAction?.Invoke(this, EventArgs.Empty);
	}

	private void Crouch_performed(InputAction.CallbackContext obj)
	{
		OnCrouchAction?.Invoke(this, EventArgs.Empty);
	}

	private void Interact_performed(InputAction.CallbackContext obj)
	{
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	private void OnDisable()
	{
		_playerActionsMap.Disable();
	}

	public Vector2 GetMovementVector2Normalized()
	{
		Vector2 inputVector = _playerActionsMap.PlayerAction.Move.ReadValue<Vector2>();
		return inputVector.normalized;
	}
	
}
