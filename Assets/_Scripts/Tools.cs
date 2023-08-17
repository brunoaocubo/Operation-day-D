using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Tools : MonoBehaviour 
{
	[SerializeField] private ToolsType toolsType;
	[SerializeField] private GameObject[] tool;
	[SerializeField] private float distanceRay = 1f;
	[SerializeField] private float damageInsecticide = 5f;

	[SerializeField] private HoldButton[] holdButton;

	private Camera _mainCamera;
	private Ray _ray;
	private RaycastHit _hitInfo;

	private void Start()
	{
		_mainCamera = Camera.main;

		foreach (var item in tool)
		{
			item.SetActive(false);
		}
	}

	private void Update()
	{
		_ray = _mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		UseTools();
	}

	public void EquipInsecticide() 
	{
		SetActiveTool(0);
		toolsType = ToolsType.Insecticide;
	}

	public void EquipBleach()
	{
		SetActiveTool(1);
		toolsType = ToolsType.Bleach;
	}

	private void SetActiveTool(int index)
	{
		foreach (var item in tool)
		{
			item.SetActive(false);
		}

		tool[index].SetActive(true);
	}

	private void UseTools()
	{	
		Physics.Raycast(_ray, out _hitInfo, distanceRay);
		foreach (var item in holdButton) 
		{
			if (!item.IsPressed)
			break;

			if(toolsType == ToolsType.Insecticide) 
			{
				if (_hitInfo.collider != null)
				{
					if (_hitInfo.collider.TryGetComponent(out Larva larva))
					{
						larva.TakeDamage(damageInsecticide);
					}
				}
			}
			
			if(toolsType == ToolsType.Bleach) 
			{

			}
		}
	}
}
