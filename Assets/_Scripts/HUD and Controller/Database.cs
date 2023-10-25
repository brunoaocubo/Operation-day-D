using UnityEngine;

public class Database : MonoBehaviour
{
    private float musicVolume;
    private float effectVolume;
    private int flyerCollected;

    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public float EffectVolume { get => effectVolume; set => effectVolume = value; }
    public int FlyerCollected { get => flyerCollected; set => flyerCollected = value; }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
