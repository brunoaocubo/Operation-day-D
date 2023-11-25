using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	private void Awake()
	{
#if !UNITY_EDITOR
		Application.targetFrameRate = 31;
#endif

		if (instance == null) 
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{

	}

	public void LoadScene(int sceneId) 
	{
		SceneManager.LoadSceneAsync(sceneId);
	}
	/*
	private void OnApplicationQuit()
	{
		int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
		Debug.Log(lastSceneIndex);
		PlayerPrefs.SetInt("LastScene", lastSceneIndex);
		PlayerPrefs.Save();
	}*/

	public int CheckSceneIndex() 
	{
		return SceneManager.GetActiveScene().buildIndex;
	}
}
