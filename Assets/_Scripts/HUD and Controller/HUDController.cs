using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    private bool openMap = false;
    private bool openQuests = false;

    [Header("Link Objects UI")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sensibilitySlider;
    [SerializeField] private GameObject mapUI_img;
    [SerializeField] private GameObject questUI_img;
    [SerializeField] private GameObject flyerBT;

    [Header("Mixers")]
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer effectMixer;
   
    [SerializeField] private Button continueGame_btn;

    [Header("Menu")]
    [SerializeField] private Image outlineBT;
    [SerializeField] private TextMeshProUGUI outlineTextBT;
    [SerializeField] private Sprite outlineDefault;
    [SerializeField] private Sprite outlineSelected;

    private int outlineEnabled = 0;


	private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            outlineEnabled = PlayerPrefs.GetInt("Outline");
            if(outlineEnabled == 0)
            {
                outlineEnabled = 1;
            }
            else
            {
                outlineEnabled = 0;
            }
            OutlineBT();
        }
            
        if (continueGame_btn != null)
        {
			if (PlayerPrefs.GetInt("TutorialComplete") == 1)
			{
				continueGame_btn.interactable = true;
			}
			else
			{
				continueGame_btn.interactable = false;

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
        flyerBT.SetActive(!openQuests);
    }

    public void PlayBT()
    {
        GameManager.levelsComplete = 0;
        GameManager.instance.LoadScene(9);
    }

    public void ContinueGame() 
    {
        GameManager.instance.LoadScene(2);
    }

    public void QuitBT()
    {
        Application.Quit();
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicMixer.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void SetEffectVolume(float effectVolume)
    {
        effectMixer.SetFloat("effectVolume", effectVolume);
        PlayerPrefs.SetFloat("effectVolume", effectVolume);
	}

    public void OptionsFadeIn(CanvasGroup canvasGroup, RectTransform rectTransform, float fadeTime) 
    {
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(-1000f, rectTransform.localPosition.y, 0f);
        rectTransform.DOAnchorPos(new Vector2(0, 0f), fadeTime, false).SetEase(Ease.OutQuint);
        canvasGroup.DOFade(1, fadeTime);
    }

    public void OptionsFadeOut(CanvasGroup canvasGroup, RectTransform rectTransform, float fadeTime) 
    {
		canvasGroup.alpha = 1f;
		rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
		canvasGroup.DOFade(0, fadeTime);
	}

    #region Menu
    public void JogosDigitaisBT()
    {
        Application.OpenURL("https://portal.ifba.edu.br/lauro-de-freitas/menu-cursos/superior/tec-jogos-digitais/jogos-digitais");
    }

    public void IFBA_BT()
    {
        Application.OpenURL("https://portal.ifba.edu.br/");
    }

    public void SesabBT()
    {
        Application.OpenURL("https://www.saude.ba.gov.br/");
    }

    public void GovBT()
    {
        Application.OpenURL("https://www.gov.br/pt-br");
    }

	public void OutlineBT()
	{
		if (outlineEnabled == 0)
		{
			outlineEnabled = 1;
			outlineBT.sprite = outlineSelected;
			outlineTextBT.text = "DESATIVAR  \n BORDA \n (OBJETOS)";
		}
		else
		{
			outlineEnabled = 0;
			outlineBT.sprite = outlineDefault;
			outlineTextBT.text = "ATIVAR \n BORDA \n (OBJETOS)";
		}

		PlayerPrefs.SetInt("Outline", outlineEnabled);
	}
	#endregion

	public void SensibilitySlider()
    {
        PlayerPrefs.SetFloat("sensibility", sensibilitySlider.value);
    }

    public void LoadScene(int value)
    {
        SceneManager.LoadScene(value);
    }
}
