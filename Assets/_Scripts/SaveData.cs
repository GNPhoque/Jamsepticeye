using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class SaveData : ScriptableObject
{
	public int day;
	public float gold;
	public float glory;
	public float gloryGained;
	public float gloryTarget;
	public float hype;
	public bool lastExecFail;
	public bool watchLore;
	public bool wasSavedFromExecution;
	public bool isComingBackFromExecution;
	public EMachines selectedMachine;
	public List<MachineSO> machines;
	public List<MachineUpgradeSO> upgrades;

	[ContextMenu("Init")]
	public void Init()
	{
		day = 0;
		gold = 0;
		glory = 10;
		gloryGained = 0;
		gloryTarget = 0;
		hype = 0;
		lastExecFail = false;
		watchLore = true;
		wasSavedFromExecution = false;
		isComingBackFromExecution = false;

		foreach (var machine in machines)
		{
			machine.gloryReward = machine.baseGloryReward;
			machine.goldReward = machine.baseGoldReward;
			machine.hypeReward = machine.baseHypeReward;

			machine.roll.perfect = machine.baseRoll.perfect;
			machine.roll.ok = machine.baseRoll.ok;
			machine.roll.cross = machine.baseRoll.cross;
		}

		foreach (var upgrade in upgrades)
		{
			upgrade.isActive = false;
		}
	}
}
