using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private bool openMap = false;
    private bool questUI = false;
    [SerializeField] private GameObject mapUI_img;
    [SerializeField] private GameObject questUI_img;

    public void MapActive()
    {
        openMap = !openMap;
        mapUI_img.SetActive(openMap);
    }
    public void QuestUIActive()
    {
        questUI = !questUI;
        questUI_img.SetActive(questUI);
    }

}
