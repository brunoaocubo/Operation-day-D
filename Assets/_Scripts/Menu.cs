using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer effectMixer;
    private GameObject database;


    private void Awake()
    {
        if(FindObjectOfType<Database>()==null)
        {
            database = new GameObject();
            database.name = "database";
            database.AddComponent<Database>();
        }
    }

    public void PlayBT()
    {
        SceneManager.LoadScene("level 0");
    }

    public void QuitBT()
    {
        Application.Quit();
    }

    public void SesabBT()
    {
        Application.OpenURL("https://www.saude.ba.gov.br/");
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicMixer.SetFloat("musicVolume", musicVolume);
        database.GetComponent<Database>().MusicVolume = musicVolume;

    }

    public void SetEffectVolume(float effectVolume)
    {
        effectMixer.SetFloat("effectVolume", effectVolume);
        database.GetComponent<Database>().EffectVolume = effectVolume;
    }
}
