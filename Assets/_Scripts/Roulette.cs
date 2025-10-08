using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
	[SerializeField]
	private ExecutionManager executionManager;
	[SerializeField]
	private List<Image> slots;
	[SerializeField]
	private Image handle;

	[SerializeField]
	Sprite handleOnSprite;

	[SerializeField]
	Sprite perfectSprite;
	[SerializeField]
	Sprite successSprite;
	[SerializeField]
	Sprite failSprite;

	[SerializeField]
	private float rouletteDuration;
	[SerializeField]
	private float timeBetweenRolls;

	[SerializeField]
	private int loseScore;
	[SerializeField]
	private float winScore;
	[SerializeField]
	private float perfectScore;

	private WaitForSeconds wfs;
	public ERouletteOutcome outcome;
	public int result;
	private bool started;

	private void Start()
	{
		wfs = new WaitForSeconds(timeBetweenRolls);
	}

	[ContextMenu("StartRoulette")]
	public void StartRoulette()
	{
		if (started)
		{
			return;
		}

		AudioManager.instance.PlayClic();
		handle.sprite = handleOnSprite;
		executionManager.ShowExecutionnerActive();

		started = true;
		executionManager.saveData.gloryGained = 0;
		StartCoroutine(ProcessRoulette());

		executionManager.HideArrow();
	}

	private IEnumerator ProcessRoulette()
	{
		float currentDuration = 0f;
		result = 0;
		RouletteList roll = executionManager.machine.roll;

		while (currentDuration < rouletteDuration)
		{
			//HIDE
			foreach (var slot in slots)
			{
				slot.gameObject.SetActive(false);
			}
			//WAIT
			currentDuration += timeBetweenRolls;
			yield return wfs;
			//SHOW
			result = 0;
			for (int i = 0; i < slots.Count; i++)
			{
				Image slot = slots[i];
				slot.gameObject.SetActive(true);
				float rng = Random.value;

				int total = 0;
				total += roll.perfect;
				total += roll.ok;
				total += roll.cross;
				rng *= total;

				if (rng < roll.perfect)
				{
					slot.sprite = perfectSprite;
					result += 2;
				}
				else if (rng < roll.perfect + roll.ok)
				{
					slot.sprite = successSprite;
					result += 1;
				}
				else
				{
					slot.sprite = failSprite;
				}
			}
			//WAIT
			currentDuration += timeBetweenRolls;
			yield return wfs;
			//WAIT
			currentDuration += timeBetweenRolls;
			yield return wfs;
		}

		if (executionManager is PlyerExecutionManager && executionManager.saveData.wasSavedFromExecution)
		{
			foreach (var slot in slots)
			{
				slot.sprite = failSprite;
			}
			if (result < winScore)
			{
				outcome = ERouletteOutcome.Loose;
			}
			else
			{
				outcome = ERouletteOutcome.Win;
			}

			executionManager.ShowReward();
			yield break;
		}

		if (result < loseScore)
		{
			//BIG LOOSE
			outcome = ERouletteOutcome.Fatal;
			executionManager.saveData.lastExecFail = true;
			executionManager.saveData.hype -= executionManager.machine.hypeReward;
			executionManager.saveData.gloryGained -= executionManager.machine.gloryReward;
		}
		else if (result < winScore)
		{
			//LOOSE
			outcome = ERouletteOutcome.Loose;
			executionManager.saveData.lastExecFail = true;
		}
		else if (result < perfectScore)
		{
			//WIN
			outcome = ERouletteOutcome.Win;
			executionManager.saveData.lastExecFail = false;
			executionManager.saveData.gold += executionManager.machine.goldReward;
			executionManager.saveData.hype += executionManager.machine.hypeReward;
			executionManager.saveData.gloryGained = executionManager.machine.gloryReward * (1 + executionManager.saveData.hype);
		}
		else
		{
			//BIG WIN
			outcome = ERouletteOutcome.Perfect;
			executionManager.saveData.lastExecFail = false;
			executionManager.saveData.gold += executionManager.machine.goldReward * 1.5f;
			executionManager.saveData.hype += executionManager.machine.hypeReward * 1.5f;
			executionManager.saveData.gloryGained = executionManager.machine.gloryReward * (1.5f + executionManager.saveData.hype);
		}

		executionManager.ShowReward();
	}
}
