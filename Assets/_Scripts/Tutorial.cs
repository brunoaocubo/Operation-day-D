using System.Collections;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Tutorial : MonoBehaviour
{
	[SerializeField] QuestsController quests;

	[SerializeField] private PlayableDirector CutsceneTutorial;
	[SerializeField] private float timerCount;
	[SerializeField] private TextMeshProUGUI timerCount_txt;
	[SerializeField] private Transform spawnHouse;

	[SerializeField] private Collider[] _collisionsTutorial;

	private bool _startTutorial = false;
	private bool _finishTutorial = false;

	private void Start()
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out PlayerController playerController))
		{
			CutsceneTutorial.gameObject.SetActive(true);
			CutsceneTutorial.Play();
			StartCoroutine(WaitCutscene());
			other.gameObject.transform.localRotation = Quaternion.Euler(0f, Camera.main.transform.rotation.y, 0f);
			other.gameObject.transform.position = spawnHouse.position;
			_collisionsTutorial[0].enabled = false;
		}
	}

	private void Update()
	{
		if (_startTutorial)
		{
			PlayStage();
		}

		if(quests.CheckStateQuest(0) && quests.CheckStateQuest(1)) 
		{
			//_collisionTutorial.enabled = true;
		}
		else 
		{
			//_collisionTutorial.enabled = false;
		}
	}

	private IEnumerator WaitCutscene() 
	{
		
		yield return new WaitForSeconds((float)CutsceneTutorial.duration);
		_startTutorial = true;
	}

	void PlayStage()
	{
		if (timerCount > 0)
		{
			timerCount -= Time.deltaTime;
			DisplayTime(timerCount);

			if (_finishTutorial)
			{
				timerCount_txt.text = "Tutorial concluído com sucesso!";
			}
		}
		else
		{
			timerCount = 0;
			_finishTutorial = true;
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
