using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public bool tutorialComplete = false;

	private void Awake()
	{
		if(instance == null) 
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void LoadScene(int sceneId) 
	{
		SceneManager.LoadSceneAsync(sceneId);
	}
	

	void Update()
    {

    }

    
}
