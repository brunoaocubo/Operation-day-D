using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ToolAction : MonoBehaviour 
{
	[SerializeField] private ToolType toolsType;
	[SerializeField] private GameObject[] tools;
	[SerializeField] private float distanceRay = 1f;
	[SerializeField] private float damageInsecticide = 5f;
	[SerializeField] private HoldButton[] holdButton;
	[SerializeField] private GameObject[] toolButton;
	private Camera _mainCamera;
	private Ray _ray;
	private RaycastHit _hitInfo;


	private void Start()
	{
		_mainCamera = Camera.main;

		foreach (var item in tools)
		{
			item.SetActive(false);
		}
	}

	private void Update()
	{
		_ray = _mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		Physics.Raycast(_ray, out _hitInfo, distanceRay);
		UseTools();
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
			if (!item.IsPressed)
			break;

			if(toolsType == ToolType.Insecticide) 
			{
				if (_hitInfo.collider != null)
				{
					if (_hitInfo.collider.TryGetComponent(out Larva larva))
					{
						larva.TakeDamage(damageInsecticide);
					}
				}
			}
			
			if(toolsType == ToolType.Bleach) 
			{

			}
		}
	}
	/*
	public void PerformRaycast(System.Type type) 
	{
		if (_hitInfo.collider != null)
		{
			if (_hitInfo.collider.TryGetComponent(out type))
			{
				larva.TakeDamage(damageInsecticide);
			}
		}
	}*/

	public void InteractObj() 
	{
		if(_hitInfo.collider != null) 
		{
			if (_hitInfo.collider.TryGetComponent(out Tool tool))
			{
				if (tool.toolsType == ToolType.Insecticide)
				{
					toolButton[0].gameObject.SetActive(true);
					Debug.Log("Desgraça");
				}
				if (tool.toolsType == ToolType.Bleach)
				{
					toolButton[1].gameObject.SetActive(true);
					Debug.Log("Cu");

				}
			}
		}
	}
}
