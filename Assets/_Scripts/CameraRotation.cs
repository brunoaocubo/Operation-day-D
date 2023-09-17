using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 5.0f;
	[SerializeField] private float rotationSensitivity = 1.0f;

	private float _minVerticalAngle = -80f;
	private float _maxVerticalAngle = 80f;
	private Vector2 _touchStartPos;

	private void Update()
	{
		foreach (Touch touch in Input.touches) 
		{
			if (Input.touchCount > 0)
			{
				if (touch.position.x >= Screen.width / 2)
				{
					if (touch.phase == TouchPhase.Began)
					{
						_touchStartPos = touch.position;
					}
					else if (touch.phase == TouchPhase.Moved)
					{
						// Calcula a diferen�a de posi��o entre o in�cio e a posi��o atual do toque
						float touchDeltaX = touch.position.x - _touchStartPos.x;
						float touchDeltaY = touch.position.y - _touchStartPos.y;

						touchDeltaX *= rotationSensitivity;
						touchDeltaY *= rotationSensitivity;

						Vector3 rotation = transform.localEulerAngles;
						rotation.y += touchDeltaX * rotationSpeed * Time.deltaTime;
						rotation.x -= touchDeltaY * rotationSpeed * Time.deltaTime; // Inverte o eixo Y para mover para cima/baixo
						rotation.x = Mathf.Clamp(rotation.x, _minVerticalAngle, _maxVerticalAngle);
						transform.localEulerAngles = rotation;

						// Atualiza a posi��o inicial do toque para o pr�ximo frame
						_touchStartPos = touch.position;
					}
				}
			}
		}
	}
}
