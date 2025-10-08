using System.Collections;
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
	protected Image execution;
	[SerializeField]
	protected Image executionner;
	[SerializeField]
	protected Sprite executionnerActive;
	[SerializeField]
	protected Sprite executionnerWin;
	[SerializeField]
	protected Sprite executionnerLoose;

	[SerializeField]
	protected Roulette roulette;
	[SerializeField]
	protected GameObject rewardGO;
	[SerializeField]
	protected TextMeshProUGUI rewardText;
	[SerializeField]
	protected GameObject hubButton;

	[SerializeField]
	private Sprite axeIntro;
	[SerializeField]
	private Sprite axeActive;
	[SerializeField]
	private Sprite axeWin;
	[SerializeField]
	private Sprite axeLoose;

	[SerializeField]
	protected GameObject arrow;

	public MachineSO machine;
	private bool isAxe;
	protected Coroutine blinkCoroutine;

	public static ExecutionManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		if(saveData.selectedMachine == EMachines.Beheading)
		{
			isAxe = true;
			execution.gameObject.SetActive(false);
		}

		machine = machines.First(x => x.machine == saveData.selectedMachine);
		if (isAxe)
		{
			executionner.sprite = axeIntro;
		}
		else
		{
			execution.sprite = machine.executionIntro;
		}

		blinkCoroutine = StartCoroutine(BlinkArrow());
	}

	public IEnumerator BlinkArrow()
	{
		while (true)
		{
			yield return new WaitForSeconds(.3f);
			arrow.SetActive(false);
			yield return new WaitForSeconds(.3f);
			arrow.SetActive(true);
		}
	}

	public void HideArrow()
	{
		StopCoroutine(blinkCoroutine);
		arrow.SetActive(false);
	}

	public virtual void ShowExecutionnerActive()
	{
		if (isAxe)
		{
			executionner.sprite = axeActive;
		}
		else
		{
			executionner.sprite = executionnerActive;
		}
	}

	public virtual void ShowReward()
	{
		bool victory = roulette.outcome == ERouletteOutcome.Win|| roulette.outcome == ERouletteOutcome.Perfect;

		if (!victory)
		{
			AudioManager.instance.PlayBoo();

			if (isAxe)
			{
				executionner.sprite = axeLoose;
			}
			else
			{
				execution.sprite = machine.executionBad;
				executionner.sprite = executionnerLoose;
			}
		}
		else
		{
			AudioManager.instance.PlayApplause();

			if (isAxe)
			{
				executionner.sprite = axeWin;
			}
			else
			{
				execution.sprite = machine.executionGood;
				executionner.sprite = executionnerWin;
			}
		}

		rewardGO.SetActive(true);

		switch (roulette.outcome)
		{
			case ERouletteOutcome.Fatal:
				rewardText.text = $@"Critical failure

Gold : 0
Glory : {Mathf.RoundToInt(-machine.gloryReward  * (1 + saveData.hype) * 100 ) / 100}
Hype : {Mathf.RoundToInt(-machine.hypeReward * (1 + saveData.hype) * 100 ) / 100}";
				break;
			case ERouletteOutcome.Loose:
				rewardText.text = $@"Failure

Gold : 0
Glory : 0
Hype : 0";
				break;
			case ERouletteOutcome.Win:
				rewardText.text = $@"Success

Gold : {machine.goldReward}
Glory : {Mathf.RoundToInt(machine.gloryReward * (1 + saveData.hype) * 100 ) / 100}
Hype : {Mathf.RoundToInt(machine.hypeReward * (1 + saveData.hype) * 100 ) / 100}";
				break;
			case ERouletteOutcome.Perfect:
				rewardText.text = $@"Critical success

Gold : {machine.goldReward * 1.5}
Glory : {Mathf.RoundToInt(machine.gloryReward * 1.5f * (1 + saveData.hype) * 100 ) / 100}
Hype : {Mathf.RoundToInt(machine.hypeReward * 1.5f * (1 + saveData.hype) * 100 ) / 100}";
				break;
			default:
				break;
		}

		hubButton.SetActive(true);
	}

	public virtual void GoBackToHub()
	{
		SceneLoader.instance.LoadScene(Scenes.Hub);
	}
}
