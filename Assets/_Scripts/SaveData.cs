using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class SaveData : ScriptableObject
{
	public int day;
	public int gold;
	public float glory;
	public float gloryGained;
	public float gloryTarget;
	public float hype;
	public EMachines selectedMachine;
	public List<MachineUpgradeSO> upgrades;

	public void Init()
	{
		day = 0;
		gold = 0;
		glory = 0;
		gloryTarget = 0;
		hype = 0;
		foreach (var upgrade in upgrades)
		{
			upgrade.isActive = false;
		}
	}
}
