using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Loading : MonoBehaviour
{

    [Header("Video")]
    [SerializeField] private GameObject loaderUI;
    [SerializeField] private Slider progressSlider;


    //[Header("Config")]
    //[SerializeField] private float delay;
    //[SerializeField] private float delayText;
    //[SerializeField] private float velocityRotation;

    //[Header("Link UI Components")]
    //[SerializeField] private Image postit;
    //[SerializeField] private Image circle;
    //[SerializeField] private TextMeshProUGUI loadingText;
    //[SerializeField] private GameObject loadingCanvasUI;
    //private bool loadScreen = true;

    //private float time;


    //private void Start()
    //{
    //    time = 0;
    //    loadScreen = true;
    //    StartCoroutine(AnimText());
    //}

    //private void Update()
    //{
    //    if (loadScreen)
    //    {
    //        time += Time.deltaTime;
    //        circle.rectTransform.localEulerAngles -= new Vector3(0, 0, velocityRotation * Time.deltaTime);
    //        if (time >= delay)
    //        {
    //            loadScreen = false;
    //            loadingCanvasUI.SetActive(false);
    //            this.enabled = false;
    //        }
    //    }
    //}

    //IEnumerator AnimText()
    //{
    //    for (int i = 0; loadScreen; i++)
    //    {
    //        loadingText.text = "Carregando.";
    //        yield return new WaitForSeconds(delayText);
    //        loadingText.text = "Carregando..";
    //        yield return new WaitForSeconds(delayText);
    //        loadingText.text = "Carregando...";
    //        yield return new WaitForSeconds(delayText);
    //    }
    //}

    private void Start()
    {
        StartCoroutine(LoadScene_Coroutine(0));
    }
    
    IEnumerator LoadScene_Coroutine(int index)
    {
        Debug.Log("Executei");
        progressSlider.value = 0;
        //loaderUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.9f)
            {
                progressSlider.value = 1f;
                asyncOperation.allowSceneActivation = true;
            }


        }
        //yield return null;
    }

}
