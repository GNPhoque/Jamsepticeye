using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	public MachineUpgradeSO data;
	[SerializeField]
	private Image image;

	public void Init()
	{
		image.sprite = data.isActive ? data.onSprite : data.offSprite;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (data.isActive || HubManager.instance.saveData.gold < data.cost)
		{
			return;
		}

		HubManager.instance.saveData.gold -= data.cost;
		HubManager.instance.UpdateGold();

		data.isActive = true;
		Init();

		HubManager.instance.ShowTooltip(data.description);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HubManager.instance.ShowTooltip(data.description, data.isActive ? "" : data.cost.ToString());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		HubManager.instance.HideTooltip();
	}
}
