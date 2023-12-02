using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{	
	readonly private string _isAttachInsecticide = "isAttachInsecticide";
	readonly private string _isAttachSanitaryWater = "isAttachSanitaryWater";
	readonly private string _isAttachShovel = "isAttachShovel";

	[Header("InputsSettings")]
	[SerializeField] private Inputs inputController;
	[SerializeField] private RectTransform handleJoystick;

	[Header("Config Player")]
	[SerializeField] private float moveSpeed = 500f;
	[SerializeField] private float distanceRay = 1f;
	[SerializeField] private Animator _anim;
	[SerializeField] private AudioSource stepFootstep_sfx;

	private ToolAction toolAction;
	private Rigidbody _rigidbody;
	private Vector2 _inputVector;
	private bool _isGrounded = false;

	public RectTransform HandleJoystick { get => handleJoystick; set => handleJoystick = value; }

	private void Awake()
	{
		inputController = FindAnyObjectByType<Inputs>();
		toolAction = GetComponent<ToolAction>();
	}

	private void Start()
	{
		_rigidbody = GetComponentInChildren<Rigidbody>();
		_anim = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		ChangeAnimation();

		if (HandleJoystick.transform.localPosition.magnitude <1) 
		{
			_inputVector = Vector2.zero;
		}
		else 
		{
			_inputVector = inputController.GetMovementVector2NormalizedJoystick();
		}

		if(_rigidbody.velocity.magnitude > 1) 
		{
			stepFootstep_sfx.enabled = true;
		}
		else 
		{
			stepFootstep_sfx.enabled = false;
		}
	}

    private void FixedUpdate()
    {
		Move();
	}

    private void OnDisable()
	{
		if(HandleJoystick != null) 
		{
			HandleJoystick.transform.localPosition = new Vector2(0, 0);
		}
	}

	private void Move() 
	{
		Vector3 moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);
		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0;
		Vector3 movement = (moveDir.x * Camera.main.transform.right + moveDir.z * cameraForward).normalized;
		_rigidbody.velocity = movement * moveSpeed * Time.fixedDeltaTime;


		float gravityForce = 9.81f;
		float maxFallSpeed = 9.81f;
		float multiplyGravityForce = 10f;

		_rigidbody.AddForce(Vector3.down * gravityForce * multiplyGravityForce);

		// Limite a velocidade vertical para evitar que o objeto caia muito rápido
		if (_rigidbody.velocity.y < -maxFallSpeed)
		{
			_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -maxFallSpeed * multiplyGravityForce, _rigidbody.velocity.z);
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

			if (slopeAngle > 45f)
			{
				Vector3 slopeDirection = Vector3.Cross(hit.normal, Vector3.down);
				Vector3 force = slopeDirection * gravityForce;
				_rigidbody.AddForce(force);
			}
		}
	}

	private void ChangeAnimation() 
	{
		if (toolAction.ToolsType == ToolType.Insecticide)
		{
			_anim.SetBool(_isAttachInsecticide, true);
		}
		else
		{
			_anim.SetBool(_isAttachInsecticide, false);
		}

		if (toolAction.ToolsType == ToolType.SanitaryWater)
		{
			_anim.SetBool(_isAttachSanitaryWater, true);
		}
		else
		{
			_anim.SetBool(_isAttachSanitaryWater, false);
		}

		if (toolAction.ToolsType == ToolType.Shovel)
		{
			_anim.SetBool(_isAttachShovel, true);
		}
		else
		{
			_anim.SetBool(_isAttachShovel, false);
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
