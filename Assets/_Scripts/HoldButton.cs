using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool isPressed = false;
	public bool IsPressed { get { return isPressed; } }

	public void OnPointerDown(PointerEventData eventData)
	{
		isPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPressed = false;
	}
}
