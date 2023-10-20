using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "GameProgress/New Achievement")]
public class Achievement : ScriptableObject
{
    [Header("Info")]
    public Sprite image;
    public string tittle;
    [TextArea]
    public string description;

    [Header("Parameters")]
    public int currentValue;
    public int ammountToComplete;
    public bool isCompleted;

}
