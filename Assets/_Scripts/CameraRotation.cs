using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 5.0f;
	[SerializeField] private float rotationSensitivity = 1.0f;

	[Header("Min/Max Angle")]
	[SerializeField] private Vector2 verticalAngle;
	[SerializeField] private Vector2 horizontalAngle;

	private float rotationX, rotationY;

	private void Update()
	{
		ScreenRotation();
	}

	private void ScreenRotation()
	{
		float touchDeltaPosX = 0;
		float touchDeltaPosY = 0;

		if (Input.touchCount > 0 && Input.GetTouch(0).position.x >= Screen.width / 2)
		{
			touchDeltaPosX = Input.GetTouch(0).deltaPosition.x;
			touchDeltaPosY = Input.GetTouch(0).deltaPosition.y;
		}

		touchDeltaPosX *= rotationSensitivity;
		touchDeltaPosY *= rotationSensitivity;

		rotationX += touchDeltaPosX * rotationSpeed * Time.deltaTime;
		rotationX = Mathf.Clamp(rotationX, horizontalAngle.x, horizontalAngle.y);
		rotationY -= touchDeltaPosY * rotationSpeed * Time.deltaTime;
		rotationY = Mathf.Clamp(rotationY, verticalAngle.x, verticalAngle.y);

		transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
	}
}