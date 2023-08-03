using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseIdentity : MonoBehaviour 
{
	[SerializeField]
	private int id = 1;
	public int Id { get { return id; } }

	[SerializeField]
	private GameObject[] dialogues_text;

	public void PlaySceneHouse(int houseID) 
	{
		SceneManager.LoadScene(houseID);
	}

	public void PlayDialogueHouse(int houseID) 
	{
		dialogues_text[houseID].SetActive(true);
	}
}
