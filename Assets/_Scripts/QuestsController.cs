using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] quests_txt = new TextMeshProUGUI[5];
    [SerializeField] private bool[] checkList = new bool[5];

    public void VerificarMissoes()
    {
        for (int i = 0; i < quests_txt.Length; i++)
        {
            if(checkList[i])
            {
                quests_txt[i].fontStyle = FontStyles.Strikethrough;
                quests_txt[i].color = Color.red;

            }
            else
            {
                quests_txt[i].fontStyle = FontStyles.Normal;
                quests_txt[i].color = Color.black;
            }
            
        }
    }
}
