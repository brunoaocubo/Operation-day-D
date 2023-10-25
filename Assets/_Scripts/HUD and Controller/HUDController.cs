using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private bool openMap = false;
    private bool openQuests = false;
    private Database database;
   
    private GameObject emptyObject;

    [Header("Link Objects UI")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private GameObject mapUI_img;
    [SerializeField] private GameObject questUI_img;

    [Header("Mixers")]
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer effectMixer;
   
    [SerializeField] private GameObject continueGame_btn;
    private void Start()
    {
        /*
        if (FindFirstObjectByType<Database>() != null)
        {
            database = FindFirstObjectByType<Database>();
        }
        else
        {
            emptyObject = new GameObject();
            emptyObject.name = "Database";
            emptyObject.AddComponent<Database>();
            database = emptyObject.GetComponent<Database>();
        }*/

        if(GameManager.instance.CheckSceneIndex() == 0) 
        {
			if (DataManager.Instance.fileExist)
			{
				continueGame_btn.SetActive(true);
			}
			else
			{
				continueGame_btn.SetActive(false);
			}
		}

		sfxSlider.value = PlayerPrefs.GetFloat("effectVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void MapActive()
    {
        openQuests = false;
        openMap = !openMap;
        questUI_img.SetActive(openQuests);
        mapUI_img.SetActive(openMap);
    }
    public void QuestUIActive()
    {
        openMap = false;
        openQuests = !openQuests;
        mapUI_img.SetActive(openMap);
        questUI_img.SetActive(openQuests);
    }

    public void PlayBT()
    {
        GameManager.instance.LoadScene(1);
    }

    public void ContinueGame() 
    {
        GameManager.instance.LoadScene(2);
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
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        //database.MusicVolume = musicVolume;
        //Debug.Log("Volume da Música:" + database.MusicVolume);

    }

    public void SetEffectVolume(float effectVolume)
    {
        effectMixer.SetFloat("effectVolume", effectVolume);
		PlayerPrefs.SetFloat("effectVolume", effectVolume);

		//database.EffectVolume = effectVolume;
		//Debug.Log("Volume do SFX:" + database.EffectVolume);
	}

}
