using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Flyer : MonoBehaviour
{
    [Header("Link to Canvas")]
    [SerializeField] private Image flyerImageUI;
    [SerializeField] private GameObject flyerCanvas;
    [SerializeField] private GameObject flyerBlockImage;
    [SerializeField] private TextMeshProUGUI flyerText;
    [SerializeField] private Button flyerBackBT;
    [SerializeField] private Button flyerNextBT;


    [Header("Flyers Sprite")]
    [SerializeField] private Sprite[] flyerSprite;

    private int currentFlyer = 0;
    private int[] flyerCollected = new int[6];

    private void Start()
    { 
        for(int i=0; i<flyerCollected.Length; i++)
        {
            if(i < GameManager.levelsComplete - 1) 
            {
				flyerCollected[i] = 1;
            }
        }
        UpdateFlyer();
    }

    public void NextFlyerBT()
    {
        if (currentFlyer < 5)
        {
            currentFlyer++;
            UpdateFlyer();
            flyerBackBT.interactable = true;
            if(currentFlyer>=5)
            {
                flyerNextBT.interactable = false;
            }
        }
    }


    public void BackFlyer()
    {
        if(currentFlyer>0)
        {
            currentFlyer--;
            flyerNextBT.interactable = true;
            UpdateFlyer();
            if(currentFlyer<=0)
            {
                flyerBackBT.interactable = false;
            }
        }
    }

    public void UpdateFlyer()
    {

        flyerText.text = "Panfletos: " + (currentFlyer+1) + "/6";
        flyerImageUI.sprite = flyerSprite[currentFlyer];
        if(flyerCollected[currentFlyer] == 1)
        {
            flyerBlockImage.SetActive(false);
        }
        else
        {
            flyerBlockImage.SetActive(true);
        }    
    }
}
