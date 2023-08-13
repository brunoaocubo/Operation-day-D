using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private bool openMap = false;
    [SerializeField] private GameObject map;



    public void MapAndProgressBT()
    {
        openMap = !openMap;
        if(openMap)
        {
            map.SetActive(true);
        }
        else
        {
            map.SetActive(false);
        }
    }
}
