using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Machine", menuName ="Machine")]
public class MachineSO : ScriptableObject
{
	new public string name;
	[TextArea]
	public string description;
	public EMachines machine;

	public RouletteList roll;
	public RouletteList baseRoll;

	public Sprite onSprite;
	public Sprite offSprite;

	public Sprite executionIntro;
	public Sprite executionGood;
	public Sprite executionBad;

	public float baseGoldReward;
	public float baseGloryReward;
	public float baseHypeReward;

	public float goldReward;
	public float gloryReward;
	public float hypeReward;

	public List<MachineUpgradeSO> upgrades;
}
