using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string talkerName;
    public Sprite sprite;
    public bool isRightSide;
    [TextArea]
    public string[] messages;

}