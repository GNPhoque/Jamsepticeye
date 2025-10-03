using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MachineUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private MachineSO data;
	[SerializeField]
	private Image image;
	[SerializeField]
	private List<UpgradeUI> upgrades;

	public void OnPointerEnter(PointerEventData eventData)
	{
		HubManager.instance.ShowTooltip(data.description);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		HubManager.instance.HideTooltip();
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
		}
		if(HubManager.instance.todayMachine == data.machine)
		{
			image.sprite = data.onSprite;
		}
	}
}