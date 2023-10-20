using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "GameProgress/New Quest")]
public class Quest : ScriptableObject
{
    public int idQuest;
    public int currentValue;
    public int amount;
    public bool stateQuest;
    public string questText;
}
