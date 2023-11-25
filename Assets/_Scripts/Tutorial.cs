using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private QuestController questController;
	//[SerializeField] private PlayableDirector cutsceneTutorial;
	[SerializeField] private TextMeshProUGUI timerCount_txt;
	[SerializeField] private Transform spawnPointTutorial;
	[SerializeField] private Collider[] collisionsTutorial;
	[SerializeField] private Transform door;

	[SerializeField] private GameObject UI;
	[SerializeField] private PlayerController Player;
	[SerializeField] private GameObject SubCamera;
	[SerializeField] private Accessibility accessibility;

	private bool hasTools = false;
	private bool _startTutorial = false;
	private bool _finishTutorial = false;

	private void Start()
	{
		collisionsTutorial[0].enabled = false;
		Camera.main.backgroundColor = Color.black;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (hasTools)
		{
			if (other.TryGetComponent(out PlayerController playerController))
			{
				//cutsceneTutorial.gameObject.SetActive(true);
				//cutsceneTutorial.Play();
				StartCoroutine(PlaySecondCutsceneTutorial());

				//other.gameObject.transform.localRotation = Quaternion.Euler(0f, Camera.main.transform.rotation.y, 0f);
				//other.gameObject.transform.position = spawnPointHouse.position;
				//collisionsTutorial[0].enabled = false;
			}
		}

		if (questController.CheckStateQuest(0) && questController.CheckStateQuest(1))
		{
			collisionsTutorial[0].enabled = true;
			collisionsTutorial[1].enabled = false;
			collisionsTutorial[2].enabled = false;
			hasTools = true;
		}
		else
		{
			collisionsTutorial[0].enabled = false;
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
			PlayerPrefs.SetInt("TutorialComplete", 1);
		}
	}

	private IEnumerator PlaySecondCutsceneTutorial()
	{
		//timerCount_txt.enabled = true;
		Player.HandleJoystick.localPosition = Vector3.zero;
		UI.SetActive(false);
		SubCamera.SetActive(true);

		accessibility.OutlineStateIndividual(3, true);
		yield return new WaitForSeconds(1f);
		accessibility.OutlineStateIndividual(3, false);
		yield return new WaitForSeconds(1f);
		accessibility.OutlineStateIndividual(3, true);
		yield return new WaitForSeconds(1f);
		accessibility.OutlineStateIndividual(3, false);
		yield return new WaitForSeconds(1f);
		accessibility.OutlineStateIndividual(3, true);
		yield return new WaitForSeconds(1f);

		collisionsTutorial[0].enabled = false;
		UI.SetActive(true);
		SubCamera.SetActive(false);

		//Player.SetActive(true);
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
	}*/
}
