using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProgressController : MonoBehaviour
{
    [SerializeField] private int ammountFlyer = 0;
    private int currentFlyer = 0;

    [Header("Options")]
    [SerializeField] Color[] colorSlot = new Color[2];

    [Header("Link With Canvas")]
    [SerializeField] Image flyerImageUI;
    [SerializeField] Button flyerBackBT;
    [SerializeField] Button flyerNextBT;
    [SerializeField] AchievementSlotUI[] achievementSlotUI;

    [Header("Scriptable Objects")]
    [SerializeField] Achievement[] achievement;

    [Header("Others")]
    [SerializeField] Sprite[] flyers;



    private void Awake()
    {
        for (int i=0; i<achievementSlotUI.Length; i++)
        {
            achievementSlotUI[i].tittle.text = achievement[i].tittle;
            achievementSlotUI[i].description.text = achievement[i].description;
            achievementSlotUI[i].image.sprite = achievement[i].image;
            if(achievement[i].isCompleted)
            {
                achievementSlotUI[i].slotAchiement.color = colorSlot[1];
            }
            else
            {
                achievementSlotUI[i].slotAchiement.color = colorSlot[0];
            }
        }
    }

    private void Start()
    {
        if(ammountFlyer<=0)
        {
            flyerImageUI.sprite = null;
            flyerImageUI.color = colorSlot[0];
            flyerBackBT.interactable = false;
            flyerNextBT.interactable = false;
        }
        else
        {
            flyerImageUI.sprite = flyers[0];
            flyerBackBT.interactable = false;
            if(ammountFlyer>1)
            {
                flyerNextBT.interactable = true;
            }
            else
            {
                flyerNextBT.interactable = false;
            }
            flyerImageUI.color = colorSlot[1];
        }
    }

    public void NextFlyer()
    {
        if(currentFlyer+1<ammountFlyer)
        {
            currentFlyer++;
            //flyerBackBT.interactable = true;
            if(currentFlyer+1==ammountFlyer)
            {
                flyerNextBT.interactable = false;
            }
            flyerImageUI.sprite = flyers[currentFlyer];
        }

        //Debug.Log(currentFlyer);
    }

    public void BackFlyer()
    {
        if (currentFlyer>0)
        {
            currentFlyer--;
            //flyerNextBT.interactable = true;
            if (currentFlyer == 0)
            {
                flyerBackBT.interactable = false;
            }
            flyerImageUI.sprite = flyers[currentFlyer];
            //Debug.Log(currentFlyer);
        }
       
    }
}
