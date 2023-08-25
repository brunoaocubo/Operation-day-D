using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] quests = new TextMeshProUGUI[5];
    [SerializeField] private bool[] testando = new bool[5];

    public void VerificarMissoes()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if(testando[i])
            {
                quests[i].fontStyle = FontStyles.Strikethrough;
                quests[i].color = Color.red;

            }
            else
            {
                quests[i].fontStyle = FontStyles.Normal;
                quests[i].color = Color.black;
            }
            
        }
    }
}
