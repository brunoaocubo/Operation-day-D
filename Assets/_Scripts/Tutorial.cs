using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private QuestController questController;
	[SerializeField] private TextMeshProUGUI timerCount_txt;
	[SerializeField] private Collider[] collisionsTutorial;
	[SerializeField] private Transform door;

	[SerializeField] private GameObject UI;
	[SerializeField] private PlayerController Player;
	[SerializeField] private GameObject SubCamera;
	[SerializeField] private Accessibility accessibility;

	private bool hasTools = false;

	private void Awake()
	{
		GameManager.levelsComplete = 2;
	}

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
				StartCoroutine(PlaySecondCutsceneTutorial());
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
		if (questController.CheckStateQuest(0) &&
			questController.CheckStateQuest(1) &&
			questController.CheckStateQuest(2) &&
			questController.CheckStateQuest(3) &&
			questController.CheckStateQuest(4)) 
		{
			PlayerPrefs.SetInt("TutorialComplete", 1);
		}
	}

	private IEnumerator PlaySecondCutsceneTutorial()
	{
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
	}
}
