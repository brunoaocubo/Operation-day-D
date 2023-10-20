using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRate : MonoBehaviour
{
    private float framePerSecond;
    [Range(0f,2f)]
    [SerializeField] private float delay;
    [SerializeField] private TextMeshProUGUI fpsUI;

    private void Start()
    {
        InvokeRepeating(nameof(FramePerSecond), 0, delay);   
    }

    private void FramePerSecond()
    {
        framePerSecond = 1f / Time.deltaTime;
        fpsUI.text = framePerSecond.ToString("F0");
    }
}
