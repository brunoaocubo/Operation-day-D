using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	private bool isPressed = false;  public bool IsPressed { get { return isPressed; } }
	private bool isClicked = false;  public bool IsClicked { get { return isClicked; } }


	public void OnPointerDown(PointerEventData eventData)
	{
		isPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPressed = false;
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(FrameClick());
	}

	IEnumerator FrameClick() 
	{
		isClicked = true;
		yield return new WaitForEndOfFrame();
		isClicked = false;
	}
}
