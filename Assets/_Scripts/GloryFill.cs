using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GloryFill : MonoBehaviour
{
	[SerializeField]
	private Image foreground;
	[SerializeField]
	private Image fill;
	[SerializeField]
	private RectTransform cursor;
	[SerializeField]
	private float fillSpeed;
	[SerializeField]
	float ropeMinPosition;
	[SerializeField]
	float ropeMaxPosition;

	private float startFill;

	[ContextMenu("ResetGlory")]
	public void SetGloryOnHubLoaded()
	{
		fill.fillAmount = HubManager.instance.saveData.glory / 100f;
		ChangeFillColor();
	}

	[ContextMenu("ResetTarget")]
	public void UpdateTargetGlory()
	{
		cursor.anchoredPosition = new Vector3(cursor.anchoredPosition.x, Mathf.Lerp(ropeMinPosition, ropeMaxPosition, HubManager.instance.saveData.gloryTarget / 100f));
	}

	[ContextMenu("FillGlory")]
	public void FillGlory()
	{
		StartCoroutine(Fill());
	}

	private IEnumerator Fill()
	{
		startFill = HubManager.instance.saveData.glory / 100f;
		float target = HubManager.instance.saveData.glory + HubManager.instance.saveData.gloryGained;
		fill.fillAmount = startFill;

		while (fill.fillAmount < target / 100f)
		{
			fill.fillAmount += Time.deltaTime * fillSpeed;
			ChangeFillColor();
			yield return null;
		}

		HubManager.instance.saveData.glory = target;
		HubManager.instance.saveData.gloryGained = 0;
		fill.fillAmount = target / 100f;
		ChangeFillColor();
	}

	public void ChangeFillColor()
	{
		fill.color = fill.fillAmount >= HubManager.instance.saveData.gloryTarget / 100f ? Colors.green : Colors.blood;
	}
}
