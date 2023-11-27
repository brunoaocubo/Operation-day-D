using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneIntro : MonoBehaviour
{
    [Header ("Config")]
    [SerializeField] private float delayCheck;
    [Range(0, 10f)]
    [SerializeField] private float dragDistance;
    [SerializeField] private Sprite[] historyImage;
  

    [Header("UI Reference")]
    [SerializeField] private Image cutsImageUI;
    [SerializeField] private GameObject cutsceneBT;
    private int currentImage = 0;
    public bool checkTouch = true;

    private void Awake()
    {
        cutsceneBT.SetActive(false);
    }

    void Update()
    {
        CheckTouch();
    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                if (checkTouch && touch.deltaPosition.x < -dragDistance)
                {
                    if (currentImage < 2)
                    {
                        checkTouch = false;
                        currentImage++;
                        cutsImageUI.sprite = historyImage[currentImage];
                        StartCoroutine(CheckTouchDelay());
                    }
                }
                if (checkTouch && touch.deltaPosition.x > dragDistance)
                {
                    if (currentImage > 0)
                    {
                        checkTouch = false;
                        currentImage--;
                        cutsImageUI.sprite = historyImage[currentImage];
                        StartCoroutine(CheckTouchDelay());

                    }
                }

                if (currentImage == 2)
                {
                    cutsceneBT.SetActive(true);
                }
                else
                {
                    cutsceneBT.SetActive(false);
                }
            }
        }
    }
    IEnumerator CheckTouchDelay()
    {
        yield return new WaitForSeconds(delayCheck);
        checkTouch = true;
    }

    public void CutsceneIntroBT(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
