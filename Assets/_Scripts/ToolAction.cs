using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ToolAction : MonoBehaviour 
{
	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private VisualEffect sprayEffect;
	[SerializeField] private float distanceRay = 1f;
	[SerializeField] private float damageInsecticide = 5f;
	[SerializeField] private ToolType toolsType;
	[SerializeField] private GameObject[] tools;
	[SerializeField] private HoldButton[] holdButton;
	[SerializeField] private GameObject[] toolButton;

	private CinemachineImpulseSource _impulseSource;
	private Camera _mainCamera;
	private Ray _ray;
	private RaycastHit _hitInfo;

	private bool hideButtonsTutorial = false;
	private void Start()
	{
		_impulseSource = virtualCamera.GetComponent<CinemachineImpulseSource>();
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

		UseTools();

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

	private void SetActiveTool(int index)
	{
		foreach (var item in tools)
		{
			item.SetActive(false);
		}

		tools[index].SetActive(true);
	}

	private void UseTools()
	{
		foreach (var item in holdButton) 
		{
			if (item.IsPressed) 
			{
				switch (toolsType)
				{
					case ToolType.Insecticide:
						if (_hitInfo.collider != null)
						{
							if (_hitInfo.collider.TryGetComponent(out Larva larva))
							{
								larva.TakeDamage(damageInsecticide);
								_impulseSource.GenerateImpulse();
							}
						}
					break;
				}
			}
		}
	}

	public void TutorialGetTool() 
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
