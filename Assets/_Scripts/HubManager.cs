using System.Collections.Generic;
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

	public EMachines todayMachine;

	public static HubManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
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

		//Bottom Left Glory and gold display
		gloryFill.SetGloryOnHubLoaded();
		gloryFill.UpdateTargetGlory();
		gloryFill.FillGlory();
		UpdateGold();
	}

	public void UpdateGold()
	{
		goldText.text = saveData.gold.ToString();
	}

	public void ShowTooltip(string text, string cost = "")
	{
		HideTooltip();
		tooltipGO.SetActive(true);
		tooltipText.text = text;
		if(!string.IsNullOrEmpty(cost))
		{
			ShowCost(cost);
		}
	}

	private void ShowCost(string cost)
	{
		tooltipCostGO.SetActive(true);
		tooltipCostText.text = cost;
		tooltipCostText.color = saveData.gold > int.Parse(cost) ? Colors.white : Colors.blood;
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
