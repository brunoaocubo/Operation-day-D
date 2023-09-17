using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ToolAction : MonoBehaviour 
{
	[SerializeField] private Material waterClean_mat;
	[SerializeField] private ToolType toolsType;
	[SerializeField] private BoxCollider insecticideDamageBox;
	//[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private VisualEffect sprayEffect;
	[SerializeField] private GameObject[] tools;
	[SerializeField] private GameObject[] toolButton;
	[SerializeField] private HoldButton[] holdButton;
	[SerializeField] private float distanceRay = 1f;

	//private CinemachineImpulseSource _impulseSource;
	private Camera _mainCamera;
	private Ray _ray;
	private RaycastHit _hitInfo;

	private bool hideButtonsTutorial = false;
	private void Start()
	{
		//_impulseSource = virtualCamera.GetComponent<CinemachineImpulseSource>();
		_mainCamera = Camera.main;

		foreach (var item in tools)
		{
			item.SetActive(false);
		}

		if(hideButtonsTutorial == false) 
		{
			foreach (var item in toolButton)
			{
				item.gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
		_ray = _mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		Physics.Raycast(_ray, out _hitInfo, distanceRay);
		Debug.DrawRay(_ray.origin, _ray.direction * distanceRay, color: Color.red);

		ActionTool();
		
		if (holdButton[0].IsPressed && toolsType == ToolType.Insecticide) 
		{
			sprayEffect.SetFloat("SprayRate", 32);
		}
		else 
		{
			sprayEffect.SetFloat("SprayRate", 0);
		}
		
	}

	public void EquipInsecticide() 
	{
		SetActiveTool(0);
		toolsType = ToolType.Insecticide;
	}

	public void EquipBleach()
	{
		SetActiveTool(1);
		toolsType = ToolType.Bleach;
	}

	private void ActionTool()
	{
		foreach (var item in holdButton) 
		{
			if (item.IsPressed) 
			{
				switch (toolsType)
				{
					case ToolType.Insecticide:
						if (!insecticideDamageBox.enabled) 
						{
							insecticideDamageBox.enabled = true;
						}
					break;
					case ToolType.Bleach:
						if(_hitInfo.collider.gameObject != null) 
						{
							if(_hitInfo.collider.gameObject.layer == 11) 
							{
								GetComponent<MeshRenderer>().material = waterClean_mat;
							}

						}
					break;
				}
			}
			else 
			{
				insecticideDamageBox.enabled = false;
			}
		}
	}

	private void SetActiveTool(int index)
	{
		foreach (var item in tools)
		{
			item.SetActive(false);
		}

		if (!tools[index].activeInHierarchy)
			tools[index].SetActive(true);
		else
			tools[index].SetActive(false);
	}

	public void Tutorial_EquipTool() 
	{
		if(_hitInfo.collider != null) 
		{
			if (_hitInfo.collider.TryGetComponent(out Tool tool))
			{
				switch (tool.toolsType)
				{
					case ToolType.Insecticide:
						toolButton[0].gameObject.SetActive(true);
						break;

					case ToolType.Bleach:
						toolButton[1].gameObject.SetActive(true);
						break;
				}
			}
			hideButtonsTutorial = true;
		}
	}
}
