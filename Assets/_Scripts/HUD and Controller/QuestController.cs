using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour, IQuestController
{
    [SerializeField] private int nextLevel;
    [SerializeField] private HouseController houseController;

    [Header("Link Objects/UI")]
    [SerializeField] private TextMeshProUGUI questsProgressUI;
    [SerializeField] private GameObject questSlot;
    [SerializeField] private TextMeshProUGUI[] quest_txt;
    [SerializeField] private Quest[] questList;

    private int questsRemaing;
    private int questsCompleted = 0; 


    private void Start()
    {
        questsRemaing = questList.Length;
        questsProgressUI.text = "Missões: " + questsCompleted + "/" + questsRemaing;

        for(int i=0; i<questList.Length; i++)
        {
			questList[i].currentValue = 0;
			if (questList[i].currentValue >= questList[i].amount) 
            {
                questList[i].stateQuest = true;
            }
            else 
            {
                questList[i].stateQuest = false;
            }

			quest_txt[i].text = questList[i].currentValue + "/" + questList[i].amount + " " + questList[i].questText;
		}
    }
    public void UpdateProgressQuest(int questID, int plus)
    {
        int i = 0;

        foreach(Quest quest in questList)
        {
            if(quest.idQuest == questID)
            {
                if (quest.currentValue < quest.amount)
                {
                    quest.currentValue += plus;
                    quest_txt[i].text = questList[i].currentValue + "/" + questList[i].amount + " " + questList[i].questText;
                    
                    if (quest.currentValue >= quest.amount)
                    {
                        quest.stateQuest = true;
                        quest_txt[i].fontStyle = FontStyles.Strikethrough;
                        quest_txt[i].color = Color.red;
						questsCompleted++;
						questsProgressUI.text = "Missões: " + questsCompleted + "/" + questsRemaing;

					}
                }
            }
            i++;
        }

        if(questsCompleted >= questList.Length)
        {
            StartCoroutine(StageCompleted());
            GameManager.instance.UpdateHouseState(houseController.NameCurrentHouse, 1);
        }
    }

    public bool CheckStateQuest(int questID) 
    {
		bool state = false;
		foreach (Quest quest in questList) 
        {
            if(quest.idQuest == questID)
            {
                state = quest.stateQuest;
			}
        }
		return state;
    }

    IEnumerator StageCompleted()
    {
        for(int i=0; i<3; i++)
        {
            Debug.Log("Você será movido em: " + i);
            yield return new WaitForSeconds(1f);
        }
        GameManager.instance.LoadScene(nextLevel);
    }
}
