using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractableObject
{
	[Header("UI")]
	[SerializeField] private GameObject interactable_txt;

	public void SetEnableUI()
	{
		if (interactable_txt != null)
		{
			interactable_txt.SetActive(true);
		}
	}

	void Start()
    {
        
    }


    void Update()
    {
        
    }
}
