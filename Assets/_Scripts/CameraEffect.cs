using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CameraEffect : MonoBehaviour
{
	[Header("CameraEffectElastic")]
	[SerializeField][Range(0f, 90f)] private float cameraDefaultFOV = 60;
	[SerializeField][Range(0f, 90f)] private float cameraTargetFOV = 70;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	public void ExecuteElasticFov() 
	{
		mainCamera.fieldOfView += 1f * Time.deltaTime;

		if (mainCamera.fieldOfView >= cameraTargetFOV)
		{
			mainCamera.fieldOfView = cameraTargetFOV;
		}
	}

	public void ReturnDefaultFOV() 
	{
		mainCamera.fieldOfView -= 0.3f;

		if (mainCamera.fieldOfView <= cameraDefaultFOV)
		{
			mainCamera.fieldOfView = cameraDefaultFOV;

		}
	}
}
