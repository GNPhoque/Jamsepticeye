using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionManager : MonoBehaviour
{
	[SerializeField]
	public SaveData saveData;
	[SerializeField]
	private List<MachineSO> machines;

	[SerializeField]
	private Image execution;
	[SerializeField]
	private Image executionner;
	[SerializeField]
	private Sprite executionnerActive;
	[SerializeField]
	private Sprite executionnerWin;
	[SerializeField]
	private Sprite executionnerLoose;

	[SerializeField]
	private Roulette roulette;
	[SerializeField]
	private GameObject rewardGO;
	[SerializeField]
	private TextMeshProUGUI rewardText;
	[SerializeField]
	private GameObject hubButton;

	public MachineSO machine;

	public static ExecutionManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		machine = machines.First(x => x.machine == saveData.selectedMachine);
		execution.sprite = machine.executionIntro;
	}

	public void ShowExecutionnerActive()
	{
		executionner.sprite = executionnerActive;
	}

	public void ShowReward()
	{
		if(roulette.outcome == ERouletteOutcome.Fatal || roulette.outcome == ERouletteOutcome.Loose)
		{
			execution.sprite = machine.executionBad;
			executionner.sprite = executionnerLoose;
		}
		else
		{
			execution.sprite = machine.executionGood;
			executionner.sprite = executionnerWin;
		}

		rewardGO.SetActive(true);
		rewardText.text = $@"Success : {roulette.outcome}
Score : {roulette.result}
Glory : {saveData.gloryGained}
Hype : {saveData.hype}";
		hubButton.SetActive(true);
	}

	public void GoBackToHub()
	{
		SceneLoader.instance.LoadScene(Scenes.Hub);
	}
}
