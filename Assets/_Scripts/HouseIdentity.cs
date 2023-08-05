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

	public void PlaySceneHouse(int houseID) 
	{
		SceneManager.LoadScene(houseID);
	}
}
