using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearQuests : MonoBehaviour
{
    [SerializeField] private Quest[] quests;

	private void Awake()
	{
		if (PlayerPrefs.GetFloat("sensibility") <= 0f)
			PlayerPrefs.SetFloat("sensibility", 1f);
	}

	public void ClearData()
    {
        PlayerPrefs.SetInt("TutorialComplete", 0);
        PlayerPrefs.SetFloat("textSize", 1.25f);

		for (int i=0; i<quests.Length; i++)
        {
            quests[i].currentValue = 0;
            quests[i].stateQuest = false;
        }
    }
}
