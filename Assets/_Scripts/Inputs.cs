using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
	private PlayerInput _playerActionsMap;

	[SerializeField]
	private Joystick leftJoystick;
	public event EventHandler OnJumpAction;
	public event EventHandler OnInteractAction;

	private void Awake()
	{
		_playerActionsMap = new PlayerInput();
	}

	private void OnEnable()
	{
		_playerActionsMap.Enable();
		_playerActionsMap.PlayerAction.Jump.performed += Jump_performed;
		_playerActionsMap.PlayerAction.Interact.performed += Interact_performed;
	}

	private void Jump_performed(InputAction.CallbackContext obj)
	{
		OnJumpAction?.Invoke(this, EventArgs.Empty);
	}

	private void Interact_performed(InputAction.CallbackContext obj)
	{
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVector2NormalizedKeyboard()
	{
		Vector2 inputVector = _playerActionsMap.PlayerAction.Move.ReadValue<Vector2>();
		return inputVector.normalized;
	}

	public Vector2 GetMovementVector2NormalizedJoystick()
	{
		Vector2 inputVector = new Vector2(leftJoystick.Direction.x, leftJoystick.Direction.y);
		return inputVector;
	}

}
