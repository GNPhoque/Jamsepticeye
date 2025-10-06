using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource music;
	[SerializeField]
	private AudioSource sfx;

	[SerializeField]
	private AudioClip[] clicClips;
	[SerializeField]
	private AudioClip[] applauseClips;
	[SerializeField]
	private AudioClip[] booClips;

	public static AudioManager instance;

	private void Awake()
	{
		if(instance!= null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void PlayClic()
	{
		sfx.PlayOneShot(GetRandomClip(clicClips));
	}

	public void PlayApplause()
	{
		sfx.PlayOneShot(GetRandomClip(applauseClips));
	}

	public void PlayBoo()
	{
		sfx.PlayOneShot(GetRandomClip(booClips));
	}

	private AudioClip GetRandomClip(AudioClip[] clips)
	{
		return clips[Random.Range(0, clips.Length)];
	}

	public void Mute()
	{
		music.mute = !music.mute;
		sfx.mute = !sfx.mute;
	}
}
