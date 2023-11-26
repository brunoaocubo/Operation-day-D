using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using System.Collections;

public class ToolAction : MonoBehaviour
{
	[SerializeField] private ToolType toolsType;
	[SerializeField] private float distanceRay = 1f;

	[SerializeField] private BoxCollider insecticideDamageBox;
	[SerializeField] private VisualEffect sprayEffect;
	[SerializeField] private Material waterClean_mat;
	[SerializeField] private Material clayClean_mat;

	[Header("Tools Settings")]
	[SerializeField] private GameObject[] tools;
	[SerializeField] protected GameObject[] toolButton;
	[SerializeField] private HoldButton[] holdButton;

	[Header("Sounds FX")]
	[SerializeField] private AudioSource insecticideSpray_sfx;
	[SerializeField] private AudioSource cleanWater_sfx;
	[SerializeField] private AudioSource insecticideEquip_sfx;
	[SerializeField] private AudioSource sanitaryWaterEquip_sfx;
	[SerializeField] private AudioSource shovelEquip_sfx;

	[SerializeField] protected AudioSource pickupItem_sfx;

	[Header("Quests")]
	//[SerializeField] private int idQuest;
	protected QuestController questController;

	[Header("CameraElastic")]
	[SerializeField] private CameraEffect cameraEffect;

	private Camera mainCamera;
	private Ray _ray;
	protected RaycastHit _hitInfo;
	protected bool hideButtonsTutorial = true;

	public ToolType ToolsType => toolsType;


	private void Awake()
	{
		mainCamera = Camera.main;
		holdButton[0] = FindAnyObjectByType<HoldButton>();
		questController = FindAnyObjectByType<QuestController>();
	}

	private void Start()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		if (scene == 1 || scene == 2)
        {
			foreach (var item in tools)
			{
				item.SetActive(false);
			}

			if (hideButtonsTutorial == true)
			{
				foreach (var item in toolButton)
				{
					item.gameObject.SetActive(false);
				}
			}
		}
    }

	private void Update()
	{
		ToolActionRaycast();

		if (toolsType == ToolType.Insecticide)
		{
			UseInsecticide();
		}
	}

	public void OpenDoor() 
	{
		if(_hitInfo.collider != null) 
		{
			if(_hitInfo.collider.TryGetComponent(out DoorRotation door))
			{
				door.RotationWithSmooth();
			}
			
			if(_hitInfo.collider.TryGetComponent(out Outline outline)) 
			{
				outline.DesactiveOutline();
			} 
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
			insecticideSpray_sfx.enabled = true;
		}
		else
		{
			cameraEffect.ReturnDefaultFOV();
			sprayEffect.SetFloat("SprayRate", 0);
			insecticideDamageBox.enabled = false;
			new WaitForEndOfFrame();
			insecticideSpray_sfx.enabled = false;
		}
	}
	
	public void UseSanitaryWater()
	{
		if (toolsType == ToolType.SanitaryWater)
		{
			if (_hitInfo.collider != null)
			{
				if (_hitInfo.collider.TryGetComponent(out WaterToxicIdentity waterToxicIdentity))
				{
					_hitInfo.collider.GetComponent<Collider>().enabled = false;
					_hitInfo.collider.GetComponent<Renderer>().material = new Material(waterClean_mat);
					_hitInfo.collider.GetComponentInParent<Outline>().enabled = false;
					cleanWater_sfx.Play();
					questController.UpdateProgressQuest(waterToxicIdentity.QuestID, 1);
				}
			}
		}
	}

	public void UseShovel() 
	{
		if(toolsType == ToolType.Shovel) 
		{
			if (_hitInfo.collider != null)
			{
				if (_hitInfo.collider.TryGetComponent(out WaterToxicIdentity waterToxicIdentity))
				{
					_hitInfo.collider.gameObject.GetComponent<Collider>().enabled = false;
					_hitInfo.collider.GetComponent<Renderer>().material = new Material(clayClean_mat);
					questController.UpdateProgressQuest(waterToxicIdentity.QuestID, 1);
				}
			}
		}
	}

	public void EquipInsecticide()
	{
		//A ordem das linhas é importante
		SetToolActive(0);
		if(toolsType == ToolType.Insecticide) 
		{
			insecticideEquip_sfx.Play();
		}
	}

	public void EquipSanitaryWater()
	{
		//A ordem das linhas é importante
		SetToolActive(1);
		if (toolsType == ToolType.SanitaryWater)
		{
			sanitaryWaterEquip_sfx.Play();
		}
	}

	public void EquipShovel()
	{
		//A ordem das linhas é importante
		SetToolActive(2);
		if (toolsType == ToolType.Shovel)
		{
			shovelEquip_sfx.Play();
		}
	}

	private void SetToolActive(int index)
	{
		if (!tools[index].activeInHierarchy)
			tools[index].SetActive(true);
		else
		{
			tools[index].SetActive(false);
		}

		switch (index)
		{
			case 0:
				if (tools[0].activeInHierarchy) { toolsType = ToolType.Insecticide; }
				else
					toolsType = ToolType.None;
				break;
			case 1:
				if (tools[1].activeInHierarchy) { toolsType = ToolType.SanitaryWater; }
				else
					toolsType = ToolType.None;
				break;
			case 2:
				if (tools[2].activeInHierarchy) { toolsType = ToolType.Shovel; }
				else
					toolsType = ToolType.None;
				break;
		}
	}

	private void ToolActionRaycast()
	{
		_ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		Physics.Raycast(_ray, out _hitInfo, distanceRay);
		Debug.DrawRay(_ray.origin, _ray.direction * distanceRay, color: Color.red);
	}
}
