using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum Scenes
{
	MainMenu = 0,
	Hub = 1,
	Execution = 2,
	PlayerExecution = 3,
	GameOverLoose = 4,
	GameOverWin = 5
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
		print("INIT");
		saveData.Init();
		LoadScene(Scenes.Hub);
	}
	
	public void LoadScene(int scene)
	{
		AudioManager.instance.PlayClic();
		SceneManager.LoadScene(scene);
	}

	public void LoadScene(Scenes scene)
	{
		AudioManager.instance.PlayClic();
		SceneManager.LoadScene((int)scene);
	}
}
