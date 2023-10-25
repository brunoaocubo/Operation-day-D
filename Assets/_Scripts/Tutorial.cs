using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private QuestController questController;
	[SerializeField] private PlayableDirector cutsceneTutorial;
	[SerializeField] private TextMeshProUGUI timerCount_txt;
	[SerializeField] private Transform spawnPointHouse;
	[SerializeField] private Collider[] collisionsTutorial;

	private bool hasTools = false;
	private bool _startTutorial = false;
	private bool _finishTutorial = false;

	private void Start()
	{
		collisionsTutorial[0].enabled = false;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (hasTools)
		{
			if (other.TryGetComponent(out PlayerController playerController))
			{
				cutsceneTutorial.gameObject.SetActive(true);
				cutsceneTutorial.Play();
				StartCoroutine(WaitCutscene());

				other.gameObject.transform.localRotation = Quaternion.Euler(0f, Camera.main.transform.rotation.y, 0f);
				other.gameObject.transform.position = spawnPointHouse.position;
				collisionsTutorial[0].enabled = false;
			}
		}

		if (questController.CheckStateQuest(0) && questController.CheckStateQuest(1))
		{
			collisionsTutorial[0].enabled = true;
			collisionsTutorial[1].enabled = false;
			hasTools = true;
		}
		else
		{
			collisionsTutorial[0].enabled = false;
			//Debug.Log("Not yet");
		}
	}

	private void Update()
	{
		if (_startTutorial)
		{
			//PlayStage();
		}

		if (questController.CheckStateQuest(0) &&
			questController.CheckStateQuest(1) &&
			questController.CheckStateQuest(2) &&
			questController.CheckStateQuest(3)) 
		{
			_finishTutorial = true;
		}
	}

	private IEnumerator WaitCutscene()
	{
		//timerCount_txt.enabled = true;
		yield return new WaitForSeconds((float)cutsceneTutorial.duration);
		_startTutorial = true;
	}

	/*
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
		{
			timerCount = 0;
			_finishTutorial = true;
			timerCount_txt.text = "N�o concluiu a tempo, tente novamente!";
		}
	}

	void DisplayTime(float timeToDisplay)
	{
		timeToDisplay += 1;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		timerCount_txt.gameObject.SetActive(true);
		timerCount_txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}*/
}
