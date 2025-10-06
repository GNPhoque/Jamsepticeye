using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
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
		ExecutionManager.instance.ShowExecutionnerActive();

		started = true;
		ExecutionManager.instance.saveData.gloryGained = 0;
		StartCoroutine(ProcessRoulette());

		ExecutionManager.instance.HideArrow();
	}

	private IEnumerator ProcessRoulette()
	{
		float currentDuration = 0f;
		result = 0;
		RouletteList roll = ExecutionManager.instance.machine.roll;

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

		if(result < loseScore)
		{
			//BIG LOOSE
			outcome = ERouletteOutcome.Fatal;
			ExecutionManager.instance.saveData.lastExecFail = true;
			ExecutionManager.instance.saveData.hype -= ExecutionManager.instance.machine.hypeReward;
			ExecutionManager.instance.saveData.gloryGained -= ExecutionManager.instance.machine.gloryReward;
		}
		else if (result < winScore)
		{
			//LOOSE
			outcome = ERouletteOutcome.Loose;
			ExecutionManager.instance.saveData.lastExecFail = true;
		}
		else if (result < perfectScore)
		{
			//WIN
			outcome = ERouletteOutcome.Win;
			ExecutionManager.instance.saveData.lastExecFail = false;
			ExecutionManager.instance.saveData.gold += ExecutionManager.instance.machine.goldReward;
			ExecutionManager.instance.saveData.hype += ExecutionManager.instance.machine.hypeReward;
			ExecutionManager.instance.saveData.gloryGained = ExecutionManager.instance.machine.gloryReward * (1 + ExecutionManager.instance.saveData.hype);
		}
		else
		{
			//BIG WIN
			outcome = ERouletteOutcome.Perfect;
			ExecutionManager.instance.saveData.lastExecFail = false;
			ExecutionManager.instance.saveData.gold += ExecutionManager.instance.machine.goldReward * 1.5f;
			ExecutionManager.instance.saveData.hype += ExecutionManager.instance.machine.hypeReward * 1.5f;
			ExecutionManager.instance.saveData.gloryGained = ExecutionManager.instance.machine.gloryReward * (1.5f + ExecutionManager.instance.saveData.hype);
		}

		ExecutionManager.instance.ShowReward();
	}
}
