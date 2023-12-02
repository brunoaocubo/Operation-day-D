using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Accessibility : MonoBehaviour
{
    [SerializeField] private List<Outline> outlines;
    [SerializeField] private Toggle accessibilityToggle;
    [SerializeField] private Slider textSlider;

    [Header("Options")]
    [SerializeField] private TextMeshProUGUI textSizeValue;
    [SerializeField] private TextMeshProUGUI textExample;
    [SerializeField] private Image outlineImageExample;
    [SerializeField] private Sprite outlineImageDefault;
    [SerializeField] private Sprite outlineImageSelected;

    [Header("DialogueSettings")]
    [SerializeField] private TextMeshProUGUI dialogueTextSize;

	private int outlineEnabled;

    private void Start()
    {
        textSlider.value = PlayerPrefs.GetFloat("textSize");
		TextSize(PlayerPrefs.GetFloat("textSize"));

        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            outlineEnabled = PlayerPrefs.GetInt("Outline");
            if (outlineEnabled == 0)
            {
                OutlineState(false);
                accessibilityToggle.isOn = false;
            }
            else
            {
                OutlineState(true);
                accessibilityToggle.isOn = true;
            }
        }
    }
    
    public void OutlineState(bool state)
    {
        for(int i=0; i<outlines.Count; i++)
        {
            outlines[i].enabled = state;
        }

        if(state)
        {
            PlayerPrefs.SetInt("Outline", 1);

            outlineImageExample.sprite = outlineImageSelected;
        }
        else
        {
            PlayerPrefs.SetInt("Outline", 0);
            outlineImageExample.sprite = outlineImageDefault;
        }
    }

	public void OutlineStateIndividual(int outline, bool state)
	{
		for (int i = 0; i < outlines.Count; i++)
		{
			outlines[outline].enabled = state;
		}
	}

    public void TextSize(float value)
    {
        PlayerPrefs.SetFloat("textSize", value);
        textSizeValue.text = value.ToString("F2");
        textExample.fontSize = 40 * value;

        if(SceneManager.GetActiveScene().buildIndex == 2) 
        {
            dialogueTextSize.fontSize = 40 * value;
        }
    }

}
