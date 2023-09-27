using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private GameManager GameManager;
	[SerializeField] private PlayableDirector CutsceneTutorial;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out PlayerController playerController))
		{
			GameManager.Level = Level.tutorial;
			CutsceneTutorial.Play();
		}
	}
}
