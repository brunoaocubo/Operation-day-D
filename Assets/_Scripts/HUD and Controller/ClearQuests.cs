using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearQuests : MonoBehaviour
{
    [SerializeField] private Quest[] quests;
    
    public void ClearData()
    {
        PlayerPrefs.SetInt("TutorialComplete", 0);
        for (int i=0; i<quests.Length; i++)
        {
            quests[i].currentValue = 0;
            quests[i].stateQuest = false;
        }
    }
}
