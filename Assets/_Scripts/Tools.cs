using UnityEditor.PackageManager;
using UnityEngine;

public class Tools : MonoBehaviour 
{
	[SerializeField] private ToolsType toolsType;
	[SerializeField] private GameObject[] tool;
	[SerializeField] private float distanceRay = 1f;
	[SerializeField] private float damageInsecticide = 5f;

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
		KillFocus();
	}

	private void FixedUpdate()
	{
		SwitchTool();
	}

	public void SwitchTool() 
	{
		if (Input.GetKey(KeyCode.Alpha1)) 
		{
			foreach (var item in tool)
			{
				item.SetActive(false);
			}
			tool[0].SetActive(true);
			toolsType = ToolsType.Insecticide;
		}
		else if (Input.GetKey(KeyCode.Alpha2)) 
		{
			foreach (var item in tool) 
			{ 
				item.SetActive(false); 
			}
			tool[1].SetActive(true);
			toolsType = ToolsType.Bleach;
		}
	}

	public void KillFocus()
	{	
		Physics.Raycast(_ray, out _hitInfo, distanceRay);

		if(Input.GetMouseButton(0) && toolsType == ToolsType.Insecticide) 
		{	
			if (_hitInfo.collider != null)
			{
				if (_hitInfo.collider.TryGetComponent(out Larva larva))
				{
					larva.TakeDamage(damageInsecticide);
				}
			}
		}
	}


}
