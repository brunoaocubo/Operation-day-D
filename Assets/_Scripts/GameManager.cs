using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Level Level;
	[SerializeField] private float timerCount;
    [SerializeField] private TextMeshProUGUI timerCount_txt;
	public bool _finishStage = false;

    void Update()
    {
		if(Level == Level.tutorial) 
		{
			PlayStage();
		}
    }

    void PlayStage() 
    {
        if (timerCount > 0) 
        {
			timerCount -= Time.deltaTime;
			DisplayTime(timerCount);

			if (_finishStage) 
			{
				timerCount_txt.text = "Tutorial concluído com sucesso!";
			}
		}
		else
		{
			timerCount = 0;
			_finishStage = true;
			timerCount_txt.text = "Não concluiu a tempo, tente novamente!";
		}
	}

	void DisplayTime(float timeToDisplay)
	{
		timeToDisplay += 1;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		timerCount_txt.gameObject.SetActive(true);
		timerCount_txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
