using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private float musicVolume;
    private float effectVolume;

    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public float EffectVolume { get => effectVolume; set => effectVolume = value; }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
