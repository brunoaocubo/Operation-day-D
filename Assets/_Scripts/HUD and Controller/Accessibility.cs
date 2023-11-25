using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accessibility : MonoBehaviour
{
    [SerializeField] private List<Outline> outlines;
    [SerializeField] private Toggle accessibilityToggle;

    private int outlineEnabled;
    private void Start()
    {

        /*
        outlineEnabled = PlayerPrefs.GetInt("Outline");
        if(outlineEnabled ==0)
        {
            OutlineState(false);
            accessibilityToggle.isOn = false;
        }
        else
        {
            OutlineState(true);
            accessibilityToggle.isOn = true;
        }*/
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
        }
        else
        {
            PlayerPrefs.SetInt("Outline", 0);
        }
    }

	public void OutlineStateIndividual(int outline, bool state)
	{
		for (int i = 0; i < outlines.Count; i++)
		{
			outlines[outline].enabled = state;
		}
/*
		if (state)
		{
			PlayerPrefs.SetInt("Outline", 1);
		}
		else
		{
			PlayerPrefs.SetInt("Outline", 0);
		}*/
	}
}
