using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProgressController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] Color[] colorSlot = new Color[2];

    [SerializeField] AchievementSlotUI[] achievementSlotUI;

    [Header("Scriptable Objects")]
    [SerializeField] Achievement[] achievement;


    private void Awake()
    {
        for (int i = 0; i < achievementSlotUI.Length; i++)
        {
            achievementSlotUI[i].tittle.text = achievement[i].tittle;
            achievementSlotUI[i].description.text = achievement[i].description;
            //achievementSlotUI[i].image.sprite = achievement[i].image;
            //if (achievement[i].isCompleted)
            //{
            //    achievementSlotUI[i].slotAchiement.color = colorSlot[1];
            //}
            //else
            //{
            //    achievementSlotUI[i].slotAchiement.color = colorSlot[0];
            //}
        }
    }
}
