using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;


public class DataManager : MonoBehaviour
{
	public static DataManager Instance;

	private static string gameDataPath;
	public GameData gameData;

	public bool fileExist;

	private void Start()
	{
		PlayerPrefs.SetFloat("sensibility", 1f);
	}

	/*
	private void Awake()
	{
		if (Instance == null) 
		{
			Instance = this;
		}
		else 
		{
			Destroy(gameObject);
		}
		gameDataPath = Application.persistentDataPath + "/gameData.json";

		if (File.Exists(gameDataPath))
		{
			fileExist = true;
		}
		DontDestroyOnLoad(this.gameObject);
	}*/

	public void LoadGameData()
	{
		if (File.Exists(gameDataPath))
		{
			string jsonData = File.ReadAllText(gameDataPath);
			gameData = JsonConvert.DeserializeObject<GameData>(jsonData);
		}
		else
		{
			gameData = new GameData();
		}
	}

	public void SaveGameData()
	{
		JsonSerializerSettings settings = new JsonSerializerSettings
		{
			Formatting = Formatting.Indented
		};

		string jsonData = JsonConvert.SerializeObject(gameData, settings);
		File.WriteAllText(gameDataPath, jsonData);
	}
}