using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	public void StartNewGame()
	{
		SceneLoader.instance.StartNewGame();
	}

	public void BackToTitleScreen()
	{
		SceneLoader.instance.LoadScene(Scenes.MainMenu);
	}
}
