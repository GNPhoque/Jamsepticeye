using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class HubManager : MonoBehaviour
{
	[SerializeField]
	public SaveData saveData;

	[SerializeField]
	private List<MachineUI> machines;

	[SerializeField]
	private TextMeshProUGUI dayText;
	[SerializeField]
	private TextMeshProUGUI todayMachineText;

	[SerializeField]
	private GameObject loreGO;
	[SerializeField]
	private GloryFill gloryFill;

	[SerializeField]
	private TextMeshProUGUI goldText;

	[SerializeField]
	private GameObject tooltipGO;
	[SerializeField]
	private TextMeshProUGUI tooltipText;
	[SerializeField]
	private GameObject tooltipCostGO;
	[SerializeField]
	private TextMeshProUGUI tooltipCostText;

	[SerializeField]
	private int targetBase;
	[SerializeField]
	private int targetDayMult;
	[SerializeField]
	private int targetFailCompensation;

	public EMachines todayMachine;
	private bool _isHovering;

	public static HubManager instance;

	public bool isHovering { 
		get => _isHovering;
		set
		{
			_isHovering = value;
			if (!value)
			{
				HideTooltip();
			}
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		if (!saveData.watchLore)
		{
			loreGO.SetActive(false);
		}

		gloryFill.SetGloryOnHubLoaded();
		if (CheckGameOver())
		{
			return;
		}

		saveData.watchLore = false;

		dayText.text = $"DAY {++saveData.day}";

		//Top Left TODAY display
		todayMachine = (EMachines)Random.Range(1, 7);
		saveData.selectedMachine = todayMachine;
		todayMachineText.text = $"TODAY'S MACHINE : {todayMachine.ToString()}";

		//Highlight today's machine + setup upgrades
		foreach (var machine in machines)
		{
			machine.Init();
		}

		//Top Right Show today's machine details
		machines.First(x => x.data.machine == todayMachine).OnPointerEnter(null);

		//Bottom Left Glory and gold display
		UpdateGloryTarget();
		gloryFill.UpdateTargetGlory();
		gloryFill.FillGlory();
		UpdateGold();
	}

	public void HideLore()
	{
		AudioManager.instance.PlayClic();

		loreGO.SetActive(false);
	}

	private bool CheckGameOver()
	{
		if (saveData.isComingBackFromExecution)
		{
			return false;
		}

		if(saveData.glory + saveData.gloryGained >= 100)
		{
			//WIN
			SceneLoader.instance.LoadScene(Scenes.GameOverWin);
			return true;
		}
		else if (saveData.glory + saveData.gloryGained < Mathf.Floor(saveData.gloryTarget))
		{
			//LOOSE
			SceneLoader.instance.LoadScene(Scenes.PlayerExecution);
			return true;
		}
		return false;
	}

	private void UpdateGloryTarget()
	{
		if (saveData.day < 3)
		{
			return;
		}

		if (saveData.isComingBackFromExecution)
		{
			saveData.isComingBackFromExecution = false;
			saveData.gloryTarget -= targetBase * 0.70f;
			return;
		}

		float target = saveData.gloryTarget;
		// Progression linéaire plus douce au lieu d'exponentielle
		target += targetBase * 0.70f; // Réduction de 70%

		if (saveData.lastExecFail)
		{
			target += targetFailCompensation * 0.5f; // Réduction de la pénalité
		}

		saveData.gloryTarget = target;
		gloryFill.ChangeFillColor();
	}

	public void UpdateGold()
	{
		goldText.text = Mathf.Floor(saveData.gold).ToString();
	}

	public void ShowTooltip(string text, string cost = "")
	{
		StartCoroutine(ShowTooltipAfterDelay(text, cost));
	}

	IEnumerator ShowTooltipAfterDelay(string text, string cost = "")
	{
		yield return new WaitForEndOfFrame();
		HideTooltip();
		tooltipGO.SetActive(true);
		tooltipText.text = text;
		if (!string.IsNullOrEmpty(cost))
		{
			ShowCost(cost);
		}
	}

	private void ShowCost(string cost)
	{
		tooltipCostGO.SetActive(true);
		tooltipCostText.text = cost;
		tooltipCostText.color = saveData.gold > int.Parse(cost) ? Color.black : Colors.blood;
	}

	public void HideTooltip()
	{
		tooltipGO.SetActive(false);
		tooltipCostGO.SetActive(false);
	}

	public void GoToExecution()
	{
		SceneLoader.instance.LoadScene(Scenes.Execution);
	}
}
