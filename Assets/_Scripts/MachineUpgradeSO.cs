using UnityEngine;

[CreateAssetMenu(fileName = "MachineUpgrade", menuName = "MachineUpgrade")]
public class MachineUpgradeSO : ScriptableObject
{
	new public string name;
	[TextArea]
	public string description;
	public EMachines machine;
	public EUpgrades upgradeType;

	public Sprite onSprite;
	public Sprite offSprite;

	public bool isActive;
	public float cost;

	public float goldReward;
	public float gloryReward;
	public float hypeReward;
}
