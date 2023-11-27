using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour
{
	[SerializeField] private string nameCurrentHouse;

	[SerializeField] private TextMeshProUGUI timerCount_txt;
	[SerializeField] private float timerCount;
	[SerializeField] private AudioSource gameOver_music;
	[SerializeField] private int currentScene;
	private bool _finishTutorial;

	public string NameCurrentHouse { get => nameCurrentHouse; }

	void Update()
    {
		PlayStage();
	}

	void PlayStage()
	{
		if (timerCount > 0)
		{
			timerCount -= Time.deltaTime;
			DisplayTime(timerCount);
			if (_finishTutorial)
			{
				timerCount_txt.text = "Tutorial conclu�do com sucesso!";
			}
		}
		else
		{	timerCount = 0;
			_finishTutorial = true;
			gameOver_music.enabled = true;
			timerCount_txt.text = "N�o concluiu a tempo, tente novamente!";
			StartCoroutine(RestartLevel());
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

	IEnumerator RestartLevel() 
	{
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene(currentScene);
	}
}
