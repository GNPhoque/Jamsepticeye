using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	public MachineUpgradeSO data;
	[SerializeField]
	private Image image;
	[SerializeField]
	private TextMeshProUGUI text;

	public Action<MachineUpgradeSO> OnBuy;

	public void Init()
	{
		image.sprite = data.isActive ? data.onSprite : data.offSprite;
		text.color = data.isActive ? Colors.blood : Color.black;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManager.instance.PlayClic();
		if (data.isActive || HubManager.instance.saveData.gold < data.cost)
		{
			return;
		}

		HubManager.instance.saveData.gold -= data.cost;
		HubManager.instance.UpdateGold();

		data.isActive = true;
		Init();

		HubManager.instance.ShowTooltip(data.description);

		OnBuy?.Invoke(data);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HubManager.instance.isHovering = true;

		string description = data.description;
		description += Environment.NewLine;
		switch (data.upgradeType)
		{
			case EUpgrades.Skull:
				description += $"  <sprite name=skull> +1";
				break;
			case EUpgrades.HalfSkull:
				description += $"  <sprite name=half> +1";
				break;
			case EUpgrades.Glory:
				description += $"Glory : +{data.gloryReward}";
				break;
			case EUpgrades.Hype:
				description += $"Hype : +{data.hypeReward}";
				break;
			case EUpgrades.Half2Cross1:
				description += $"  <sprite name=half> +2";
				description += $"  <sprite name=cross> +1";
				break;
			case EUpgrades.Gold:
				description += $"<sprite name=coin> +{data.goldReward}";
				break;
			default:
				break;
		}

		HubManager.instance.ShowTooltip(description, data.isActive ? "" : data.cost.ToString());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//HubManager.instance.isHovering = false;
	}
}
