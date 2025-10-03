using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Machine", menuName ="Machine")]
public class MachineSO : ScriptableObject
{
	new public string name;
	[TextArea]
	public string description;
	public EMachines machine;

	public Sprite onSprite;
	public Sprite offSprite;

	public Sprite executionIntro;
	public Sprite executionGood;
	public Sprite executionBad;

	public int baseSuccessChance;
	public int baseGoldReward;
	public int baseGloryReward;
	public int baseHypeReward;

	public List<MachineUpgradeSO> upgrades;
}
