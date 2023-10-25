using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRate : MonoBehaviour
{
    //private float framePerSecond;
    //[Range(0f,2f)]
    //[SerializeField] private float delay;

    [SerializeField] private TextMeshProUGUI fpsUI;
	[SerializeField] private int targetFPS = 60;
	private float currentFPS = 0;
	private void Start()
    {
		//InvokeRepeating(nameof(FramePerSecond), 0, delay);   

		//Application.targetFrameRate = targetFPS;
	}
    
    private void FramePerSecond()
    {
        //framePerSecond = 1f / Time.deltaTime;
        //fpsUI.text = framePerSecond.ToString("F0");
    }

	private void Update()
	{
        currentFPS = (int)(1f / Time.unscaledDeltaTime);
		fpsUI.text = "FPS: " + currentFPS;
	}
}
