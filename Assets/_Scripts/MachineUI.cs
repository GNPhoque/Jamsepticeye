using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MachineUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	public MachineSO data;
	[SerializeField]
	private Image image;
	[SerializeField]
	private List<UpgradeUI> upgrades;

	public void OnPointerEnter(PointerEventData eventData)
	{
		HubManager.instance.isHovering = true;

		float bonusSkull = 0;
		float bonusHalf = 0;
		float bonusCross = 0;
		float bonusGold = 0;
		float bonusGlory = 0;
		float bonusHype = 0;

		foreach (UpgradeUI upgrade in upgrades)
		{
			if (upgrade.data.isActive)
			{
				switch (upgrade.data.upgradeType)
				{
					case EUpgrades.Skull:
						bonusSkull += 1;
						break;
					case EUpgrades.HalfSkull:
						bonusHalf += 1;
						break;
					case EUpgrades.Glory:
						bonusGlory += upgrade.data.gloryReward;
						break;
					case EUpgrades.Hype:
						bonusHype += upgrade.data.hypeReward;
						break;
					case EUpgrades.Half2Cross1:
						bonusHalf += 2;
						bonusCross += 1;
						break;
					case EUpgrades.Gold:
						bonusGold += upgrade.data.goldReward;
						break;
				}
			}
		}

		string description = data.description;
		description += $"{Environment.NewLine}  <sprite name=skull> {data.roll.perfect}";
		if (bonusSkull != 0)
		{
			description += $" ({(bonusSkull > 0 ? "+" : "-")}{bonusSkull})";
		}
		description += $"  <sprite name=half> {data.roll.ok}";
		if (bonusHalf != 0)
		{
			description += $" ({(bonusHalf > 0 ? "+" : "-")}{bonusHalf})";
		}
		description += $"  <sprite name=cross> {data.roll.cross}";
		if (bonusCross != 0)
		{
			description += $" ({(bonusCross > 0 ? "+" : "-")}{bonusCross})";
		}
		description += $"{Environment.NewLine}<sprite name=coin> {data.goldReward}";
		if (bonusGold != 0)
		{
			description += $" ({(bonusGold > 0 ? "+" : "-")}{bonusGold})";
		}
		description += $"{Environment.NewLine}Glory : {data.gloryReward}";
		if (bonusGlory != 0)
		{
			description += $" ({(bonusGlory > 0 ? "+" : "-")}{bonusGlory})";
		}
		description += $"{Environment.NewLine}Hype : {data.hypeReward}";
		if (bonusHype != 0)
		{
			description += $" ({(bonusHype > 0 ? "+" : "-")}{bonusHype})";
		}

		HubManager.instance.ShowTooltip(description);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//HubManager.instance.isHovering = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.LogWarning("Todo : override random selected machine");
	}

	public void Init()
	{
		//image.sprite = data.offSprite;
		for (int i = 0; i < upgrades.Count; i++)
		{
			upgrades[i].data = data.upgrades[i];
			upgrades[i].Init();
			upgrades[i].OnBuy += ApplyUpgrade;
		}
		if(HubManager.instance.todayMachine == data.machine)
		{
			image.sprite = data.onSprite;
		}
	}

	private void ApplyUpgrade(MachineUpgradeSO upgrade)
	{
		switch (upgrade.upgradeType)
		{
			case EUpgrades.Skull:
				data.roll.perfect += 1;
				break;
			case EUpgrades.HalfSkull:
				data.roll.ok += 1;
				break;
			case EUpgrades.Half2Cross1:
				data.roll.ok += 2;
				data.roll.cross += 1;
				break;
			default:
				break;
		}

		data.gloryReward += upgrade.gloryReward;
		data.goldReward += upgrade.goldReward;
		data.hypeReward+= upgrade.hypeReward;
	}
}