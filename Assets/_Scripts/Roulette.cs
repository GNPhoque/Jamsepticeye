using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
	[SerializeField]
	private List<Image> slots;

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

		ExecutionManager.instance.ShowExecutionnerActive();

		started = true;
		ExecutionManager.instance.saveData.hype = 0;
		ExecutionManager.instance.saveData.gloryGained = 0;
		StartCoroutine(ProcessRoulette());
	}

	private IEnumerator ProcessRoulette()
	{
		float currentDuration = 0f;
		int[] results = new int[3];

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
			for (int i = 0; i < slots.Count; i++)
			{
				Image slot = slots[i];
				slot.gameObject.SetActive(true);
				results[i] = Random.Range(0, 3);
				switch (results[i])
				{
					case 0:
						slot.sprite = failSprite;
						break;
					case 1:
						slot.sprite = successSprite;
						break;
					case 2:
						slot.sprite = perfectSprite;
						break;
					default:
						break;
				}
			}
			//WAIT
			currentDuration += timeBetweenRolls;
			yield return wfs;
			//WAIT
			currentDuration += timeBetweenRolls;
			yield return wfs;
		}

		result = 0;
		for (int i = 0; i < results.Length; i++)
		{
			result += results[i];
		}

		if(result < loseScore)
		{
			//BIG LOOSE
			outcome = ERouletteOutcome.Fatal;
			ExecutionManager.instance.saveData.hype -= ExecutionManager.instance.machine.baseHypeReward;
			ExecutionManager.instance.saveData.gloryGained -= ExecutionManager.instance.machine.baseGloryReward;
		}
		else if (result < winScore)
		{
			//LOOSE
			outcome = ERouletteOutcome.Loose;
			ExecutionManager.instance.saveData.gloryGained -= ExecutionManager.instance.machine.baseGloryReward;
		}
		else if (result < perfectScore)
		{
			//WIN
			outcome = ERouletteOutcome.Win;
			ExecutionManager.instance.saveData.gloryGained += ExecutionManager.instance.machine.baseGloryReward;
		}
		else
		{
			//BIG WIN
			outcome = ERouletteOutcome.Perfect;
			ExecutionManager.instance.saveData.hype += ExecutionManager.instance.machine.baseHypeReward;
			ExecutionManager.instance.saveData.gloryGained += ExecutionManager.instance.machine.baseGloryReward;
		}

		ExecutionManager.instance.ShowReward();
	}
}
