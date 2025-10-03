using UnityEngine;

[CreateAssetMenu(fileName = "MachineUpgrade", menuName = "MachineUpgrade")]
public class MachineUpgradeSO : ScriptableObject
{
	new public string name;
	[TextArea]
	public string description;
	public EMachines machine;

	public Sprite onSprite;
	public Sprite offSprite;

	public bool isActive;
	public int cost;
	public int durability;

	public int goldReward;
	public int gloryReward;
	public int hypeReward;
}
