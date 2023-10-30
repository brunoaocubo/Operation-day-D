using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	readonly private string _isAttachInsecticide = "isAttachInsecticide";
	readonly private string _isAttachSanitaryWater = "isAttachSanitaryWater";
	readonly private string _isAttachShovel = "isAttachShovel";

	[SerializeField] private Inputs inputController;
	[SerializeField] private RectTransform handleJoystick;

	[Header("Config Player")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float distanceRay = 1f;

	private Rigidbody _rigidbody;
	private Vector2 _inputVector;
	private Ray _ray;
	private RaycastHit _hitInfo;
	private bool _isGrounded = false;
	private Animator _anim;

	private void Start()
	{
		_rigidbody = GetComponentInChildren<Rigidbody>();
		_anim = GetComponent<Animator>();
	}

	private void Update()
	{
		ToolType toolType = GetComponent<ToolAction>().ToolsType;
		switch (toolType)
		{
			case ToolType.None:
				_anim.SetBool(_isAttachInsecticide, false);
				_anim.SetBool(_isAttachSanitaryWater, false);
				_anim.SetBool(_isAttachShovel, false);
				break;
			case ToolType.Insecticide:
				_anim.SetBool(_isAttachInsecticide, true);
				_anim.SetBool(_isAttachSanitaryWater, false);
				_anim.SetBool(_isAttachShovel, false);
				break;
			case ToolType.SanitaryWater:
				_anim.SetBool(_isAttachSanitaryWater, true);
				_anim.SetBool(_isAttachInsecticide, false);
				_anim.SetBool(_isAttachShovel, false);

				break;
			case ToolType.Shovel:
				_anim.SetBool(_isAttachShovel, true);
				_anim.SetBool(_isAttachInsecticide, false);
				_anim.SetBool(_isAttachSanitaryWater, false);
				break;
			default:
				break;
		}
		if(toolType == ToolType.Insecticide) 
		{
			_anim.SetBool(_isAttachInsecticide, true);
		}
		else
		{
			_anim.SetBool(_isAttachInsecticide, false);
		}

		//Move();
		if(handleJoystick.transform.localPosition.magnitude <1) 
		{
			_inputVector = Vector2.zero;
		}
		else 
		{
			_inputVector = inputController.GetMovementVector2NormalizedJoystick();
		}
	}

    private void FixedUpdate()
    {
		Move();
	}

    private void OnDisable()
	{
		if(handleJoystick != null) 
		{
			handleJoystick.transform.localPosition = new Vector2(0, 0);
		}
	}

	private void Move() 
	{
		Vector3 moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);
		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0;
		Vector3 movement = (moveDir.x * Camera.main.transform.right + moveDir.z * cameraForward).normalized;
		_rigidbody.velocity = movement * moveSpeed * Time.fixedDeltaTime;
	}

	public void CheckHouseID() 
	{
		_ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
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
