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
			HideLore();
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
		gloryFill.SetGloryOnHubLoaded();
		gloryFill.UpdateTargetGlory();
		gloryFill.FillGlory();
		UpdateGold();

		CheckGameOver();
		gloryFill.UpdateTargetGlory();
	}

	public void HideLore()
	{
		loreGO.SetActive(false);
	}

	private void CheckGameOver()
	{
		if(saveData.glory >= 100)
		{
			//WIN
			SceneLoader.instance.LoadScene(Scenes.GameOverWin);
		}
		else if (saveData.glory < saveData.gloryTarget)
		{
			//LOOSE
			SceneLoader.instance.LoadScene(Scenes.GameOverLoose);
		}
	}

	private void UpdateGloryTarget()
	{
		if (saveData.day < 3)
		{
			return;
		}

		float target = saveData.gloryTarget;
		target += targetBase * (1 + target / 100);
		if (saveData.lastExecFail)
		{
			target += targetFailCompensation;
		}

		saveData.gloryTarget = target;
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
