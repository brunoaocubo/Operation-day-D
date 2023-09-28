using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestsController : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject questSlot;
    [SerializeField] private TextMeshProUGUI[] quests_txt;
    //[SerializeField] private bool[] checkList = new bool[5];

    [SerializeField] private Quest[] quests;
    private GameObject textSlot;

    private void Start()
    {
        //quests_txt = new TextMeshProUGUI[quests.Length];

        for(int i=0; i<quests.Length; i++)
        {
            //CreateTextSlot(i);
            //quests_txt[i] = textSlot.GetComponent<TextMeshProUGUI>();

            quests_txt[i].text = quests[i].questText;
            Debug.Log(quests[i].questText);
        }
    }
    public void VerificarMissoes(int questID, int plus)
    {
        int i = 0;
        foreach(Quest miss in quests)
        {
            if(miss.idQuest == questID)
            {
                miss.currentValue += plus;
                if (miss.currentValue >= miss.amount)
                {
                    miss.stateQuest = true;
                    quests_txt[i].fontStyle = FontStyles.Strikethrough;
                    quests_txt[i].color = Color.red;
                }
            }
            Debug.Log(miss.stateQuest);
            i++;
        }
    }

    private void CreateTextSlot(int idQuest)
    {
        textSlot = new GameObject();
        TextMeshProUGUI tmp = textSlot.AddComponent<TextMeshProUGUI>();
        tmp.fontSize = 30;
        tmp.color = Color.black;
        textSlot.transform.SetParent(questSlot.transform);
        textSlot.name = "Quest" + idQuest;
        textSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 30);
    }
}
