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
            flyerCollected[i] = PlayerPrefs.GetInt("flyer");
        }
    }
    public void UpdateFlyer(int value)
    {

        if(value == -1 && currentFlyer>0)
        {
            currentFlyer += value;
            flyerNextBT.interactable = true;
        }

        if(value == 1 && currentFlyer<5)
        {
            currentFlyer += value;
            flyerBackBT.interactable = true;
        }

        if (currentFlyer ==0)
        {
            flyerBackBT.interactable = false;
        }

        if(currentFlyer==5)
        {
            flyerNextBT.interactable = false;
        }

        flyerText.text = "Panfletos: " + (currentFlyer+1) + "/6";
        if(flyerCollected[currentFlyer] == 1)
        {
            flyerImageUI.sprite = flyerSprite[currentFlyer];
            flyerBlockImage.SetActive(false);
        }
        else
        {
            flyerBlockImage.SetActive(true);
        }

        
    }
}
