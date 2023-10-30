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

	Camera mainCamera;
	private Vector3 cameraOriginalPosition;
	private bool isEffectRunning = false;

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

	//Atrelado ao botão de Inseticida
	public void ExecuteElasticEffect()
	{
		isEffectRunning = true;

		cameraOriginalPosition = transform.position;
		// Mover a câmera para trás
		transform.DOMove(transform.position - transform.forward * 0.2f, 0.2f)
			.SetEase(Ease.OutQuad)
			.OnComplete(() =>
			{
				// Mover a câmera de volta para a posição original
				transform.DOMove(cameraOriginalPosition, 0.2f)
					.SetEase(Ease.InQuad)
					.OnComplete(() =>
					{
						isEffectRunning = false; // Define para false quando o ciclo for concluído
					});
			});
	}
}
