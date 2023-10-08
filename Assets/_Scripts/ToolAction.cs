using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ToolAction : MonoBehaviour
{
	[SerializeField] private ToolType toolsType;
	[SerializeField] private float distanceRay = 1f;

	[SerializeField] private BoxCollider insecticideDamageBox;
	[SerializeField] private VisualEffect sprayEffect;
	[SerializeField] private Material waterClean_mat;
	[SerializeField] private Material clayClean_mat;

	[SerializeField] private GameObject[] tools;
	[SerializeField] private GameObject[] toolButton;
	[SerializeField] private HoldButton[] holdButton;

	[Header("Quests")]
	[SerializeField] private int idQuest;
	[SerializeField] private QuestsController questsController;

	private Camera mainCamera;
	private Ray _ray;
	private RaycastHit _hitInfo;
	private bool hideButtonsTutorial = true;

	[Header("CameraElastic")]
	[SerializeField] private CameraEffect cameraEffect;

	private void Awake()
	{
		mainCamera = Camera.main;	
	}

	private void Start()
	{
		foreach (var item in tools)
		{
			item.SetActive(false);
		}

		if(hideButtonsTutorial == true) 
		{
			foreach (var item in toolButton)
			{
				item.gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
		//ExecuteAction();
		ToolActionRaycast();
		if (toolsType == ToolType.Insecticide)
		{
			UseInsecticide();
		}
	}

	private void UseInsecticide() 
	{
		if (holdButton[0].IsClicked)
		{
			cameraEffect.ExecuteElasticEffect();
		}
		if (holdButton[0].IsPressed)
		{
			cameraEffect.ExecuteElasticFov();
			sprayEffect.SetFloat("SprayRate", 32);
			insecticideDamageBox.enabled = true;
		}
		else
		{
			cameraEffect.ReturnDefaultFOV();
			sprayEffect.SetFloat("SprayRate", 0);
			insecticideDamageBox.enabled = false;
		}
	}
	
	public void UseBleach()
	{
		if (toolsType == ToolType.Bleach)
		{
			if (_hitInfo.collider != null)
			{
				if (_hitInfo.collider.gameObject.layer == 11)
				{
					_hitInfo.collider.gameObject.GetComponent<Collider>().enabled = false;
					_hitInfo.collider.GetComponent<Renderer>().material = new Material(waterClean_mat);
					questsController.UpdateProgressQuest(idQuest, 1);
				}
			}
		}
		/*
		switch (toolsType)
		{
			case ToolType.Bleach:
				if (_hitInfo.collider != null)
				{
					if (_hitInfo.collider.gameObject.layer == 11)
					{
						_hitInfo.collider.gameObject.GetComponent<Collider>().enabled = false;
						_hitInfo.collider.GetComponent<Renderer>().material = new Material(waterClean_mat);
						questsController.CheckQuest(idQuest, 1);
					}

				}
				break;
			case ToolType.Shovel:
				if (_hitInfo.collider != null)
				{
					if (_hitInfo.collider.gameObject.layer == 12)
					{
						_hitInfo.collider.gameObject.GetComponent<Collider>().enabled = false;
						_hitInfo.collider.GetComponent<Renderer>().material = new Material(clayClean_mat);
						questsController.CheckQuest(idQuest, 1);
					}
				}	
				break;
		}
		
		foreach (var item in holdButton) 
		{
			if (item.IsPressed) 
			{
				
			}
			else 
			{
				insecticideDamageBox.enabled = false;
			}
		}*/
	}
	public void UseShovel() 
	{
		if(toolsType == ToolType.Shovel) 
		{
			if (_hitInfo.collider != null)
			{
				if (_hitInfo.collider.gameObject.layer == 12)
				{
					_hitInfo.collider.gameObject.GetComponent<Collider>().enabled = false;
					_hitInfo.collider.GetComponent<Renderer>().material = new Material(clayClean_mat);
					questsController.UpdateProgressQuest(idQuest, 1);
				}
			}
		}
	}
	private void ToolActionRaycast()
	{
		_ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		Physics.Raycast(_ray, out _hitInfo, distanceRay);
		//Debug.DrawRay(_ray.origin, _ray.direction * distanceRay, color: Color.red);
	}

	public void EquipInsecticide()
	{
		SetToolActive(0);
		toolsType = ToolType.Insecticide;
	}

	public void EquipBleach()
	{
		SetToolActive(1);
		toolsType = ToolType.Bleach;
	}

	public void UnlockToolButtons() 
	{
		if(_hitInfo.collider != null) 
		{
			if (_hitInfo.collider.TryGetComponent(out Tool tool))
			{
				switch (tool.toolsType)
				{
					case ToolType.Insecticide:
						toolButton[0].gameObject.SetActive(true);
						questsController.UpdateProgressQuest(0, 1);
						break;

					case ToolType.Bleach:
						toolButton[1].gameObject.SetActive(true);
						questsController.UpdateProgressQuest(1, 1);
						break;
				}
			}
			hideButtonsTutorial = false;
		}
	}

	private void SetToolActive(int index)
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
}
