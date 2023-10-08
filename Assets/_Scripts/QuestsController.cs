using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestsController : MonoBehaviour, IQuestController
{
    [Header("Config")]
    [SerializeField] private GameObject questSlot;
    [SerializeField] private TextMeshProUGUI[] quest_txt;
    [SerializeField] private Quest[] questList;
    
    //private GameObject textSlot;

    private void Start()
    {
        for(int i=0; i<questList.Length; i++)
        {
            quest_txt[i].text = questList[i].questText;
            Debug.Log(questList[i].questText);
        }
    }
    public void UpdateProgressQuest(int questID, int plus)
    {
        int i = 0;

        foreach(Quest quest in questList)
        {
            if(quest.idQuest == questID)
            {
                quest.currentValue += plus;
                if (quest.currentValue >= quest.amount)
                {
                    quest.stateQuest = true;
                    quest_txt[i].fontStyle = FontStyles.Strikethrough;
                    quest_txt[i].color = Color.red;
                }
            }
            Debug.Log(quest.stateQuest);
            i++;
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

    //private void CreateTextSlot(int idQuest)
    //{
    //    textSlot = new GameObject();
    //    TextMeshProUGUI tmp = textSlot.AddComponent<TextMeshProUGUI>();
    //    tmp.fontSize = 30;
    //    tmp.color = Color.black;
    //    textSlot.transform.SetParent(questSlot.transform);
    //    textSlot.name = "Quest" + idQuest;
    //    textSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 30);
    //}
}
