using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlyerExecutionManager : ExecutionManager
{
	[SerializeField]
	private Sprite executionWin;
	[SerializeField]
	private Sprite executionLoose;

	private bool win;

	private void Start()
	{
		blinkCoroutine = StartCoroutine(BlinkArrow());
	}

	public override void ShowExecutionnerActive()
	{
		executionner.sprite = executionnerActive;
	}

	public override void ShowReward()
	{
		win = roulette.outcome == ERouletteOutcome.Win;

		if (win)
		{
			AudioManager.instance.PlayBoo();

			execution.sprite = executionWin;
			executionner.sprite = executionnerWin;
		}
		else
		{
			AudioManager.instance.PlayApplause();

			execution.sprite = executionLoose;
			executionner.sprite = executionnerLoose;
		}

		rewardGO.SetActive(true);

		switch (roulette.outcome)
		{
			case ERouletteOutcome.Loose:
				rewardText.text = $@"Execution failed
You are safe for this time...";
				saveData.wasSavedFromExecution = true;
				saveData.isComingBackFromExecution = true;
				break;
			case ERouletteOutcome.Win:
				rewardText.text = $@"You died!";
				break;
			default:
				break;
		}

		hubButton.SetActive(true);
	}

	public override void GoBackToHub()
	{
		SceneLoader.instance.LoadScene(win ? Scenes.GameOverLoose : Scenes.Hub);
	}
}
