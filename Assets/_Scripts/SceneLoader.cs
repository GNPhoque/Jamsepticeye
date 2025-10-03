using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum Scenes
{
	MainMenu = 0,
	Hub = 1,
	Execution = 2,
	GameOver = 3
}

public class SceneLoader : MonoBehaviour
{
	[SerializeField]
	private SaveData saveData;

	public static SceneLoader instance;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void StartNewGame()
	{
		saveData.Init();
		LoadScene((int)Scenes.Hub);
	}
	
	public void LoadScene(int scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void LoadScene(Scenes scene)
	{
		SceneManager.LoadScene((int)scene);
	}
}
