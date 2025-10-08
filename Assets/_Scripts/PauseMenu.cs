using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject optionsMenu;
	[SerializeField]
	private GameObject helpMenu;

	[SerializeField]
	private Image optionsButtonImage;
	[SerializeField]
	private Image helpButtonImage;

	[SerializeField]
	private Sprite optionsNormalSprite;
	[SerializeField]
	private Sprite optionsHoverSprite;
	[SerializeField]
	private Sprite helpNormalSprite;
	[SerializeField]
	private Sprite helpHoverSprite;
	[SerializeField]
	private Sprite closeNormalSprite;
	[SerializeField]
	private Sprite closeHoverSprite;
	
	[SerializeField]
	private TextMeshProUGUI musicText;
	[SerializeField]
	private TextMeshProUGUI sfxText;

	public static PauseMenu instance;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		AudioManager.instance.UpdateMusicVolume(0.15f);
		AudioManager.instance.UpdateSfxVolume(1f);
	}

	public void OnOptionsClick()
	{
		AudioManager.instance.PlayClic();

		helpMenu.SetActive(false);
		optionsMenu.SetActive(!optionsMenu.activeSelf);
		OnOptionsHover();
		helpButtonImage.sprite = helpNormalSprite;
	}

	public void OnOptionsHover()
	{
		optionsButtonImage.sprite = optionsMenu.activeSelf ? closeHoverSprite : optionsHoverSprite;
	}

	public void OnOptionsExit()
	{
		optionsButtonImage.sprite = optionsMenu.activeSelf ? closeNormalSprite : optionsNormalSprite;
	}

	public void OnHelpClick()
	{
		AudioManager.instance.PlayClic();

		optionsMenu.SetActive(false);
		helpMenu.SetActive(!helpMenu.activeSelf);
		OnHelpHover();
		optionsButtonImage.sprite = optionsNormalSprite;
	}

	public void OnHelpHover()
	{
		helpButtonImage.sprite = helpMenu.activeSelf ? closeHoverSprite : helpHoverSprite;
	}

	public void OnHelpExit()
	{
		helpButtonImage.sprite = helpMenu.activeSelf ? closeNormalSprite : helpNormalSprite;
	}

	public void OnMusicValueChanged(float value)
	{
		AudioManager.instance.UpdateMusicVolume(value / 3f);
		musicText.text = $"{Mathf.RoundToInt(value * 100)}%";
	}
	public void OnSfxValueChanged(float value)
	{
		AudioManager.instance.UpdateSfxVolume(value);
		sfxText.text = $"{Mathf.RoundToInt(value * 100)}%";
	}
}
