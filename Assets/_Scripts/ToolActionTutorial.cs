
public class ToolActionTutorial : ToolAction
{
	public void UnlockToolButtons()
	{
		if (_hitInfo.collider != null)
		{
			if (_hitInfo.collider.TryGetComponent(out Tool tool))
			{
				switch (tool.toolsType)
				{
					case ToolType.Insecticide:
						toolButton[0].gameObject.SetActive(true);
						questController.UpdateProgressQuest(0, 1);
						break;

					case ToolType.SanitaryWater:
						toolButton[1].gameObject.SetActive(true);
						questController.UpdateProgressQuest(1, 1);
						break;

					case ToolType.Shovel:
						toolButton[2].gameObject.SetActive(true);
						questController.UpdateProgressQuest(2, 1);
						break;
				}
			}
			hideButtonsTutorial = false;
		}
	}
}
