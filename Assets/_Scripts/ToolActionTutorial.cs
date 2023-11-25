using UnityEngine;

public class ToolActionTutorial : ToolAction
{
	[SerializeField] private Accessibility accessibility;

	public void UnlockToolButtons()
	{
		if (_hitInfo.collider != null)
		{
			if (_hitInfo.collider.TryGetComponent(out Tool tool)) 
			{
				switch (tool.toolsType)
				{
					case ToolType.Insecticide:
						if (!toolButton[0].activeInHierarchy) 
						{
							toolButton[0].gameObject.SetActive(true);
							_hitInfo.collider.GetComponent<Outline>().enabled = false;
							accessibility.OutlineStateIndividual(1, true);
							questController.UpdateProgressQuest(0, 1);
						}
						pickupItem_sfx.Play();
						break;

					case ToolType.SanitaryWater:
						if (!toolButton[1].activeInHierarchy) 
						{
							toolButton[1].gameObject.SetActive(true);
							_hitInfo.collider.GetComponent<Outline>().enabled = false;
							accessibility.OutlineStateIndividual(2, true);
							questController.UpdateProgressQuest(1, 1);
						}
						pickupItem_sfx.Play();
						break;

					case ToolType.Shovel:
						if (!toolButton[2].activeInHierarchy)
						{
							toolButton[2].gameObject.SetActive(true);
							_hitInfo.collider.GetComponent<Outline>().enabled = false;
							questController.UpdateProgressQuest(2, 1);
						}
						pickupItem_sfx.Play();
						break;
				}
			}
			hideButtonsTutorial = false;
		}
	}
}
