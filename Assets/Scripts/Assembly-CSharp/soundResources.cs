using UnityEngine;

public class soundResources : MonoBehaviour
{
	private AudioClip clip;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void play(string str)
	{
		clip = (AudioClip)Resources.Load(str, typeof(AudioClip));
		PlayAudioClip();
	}

	public void PlayAudioClip()
	{
		if (clip == null)
		{
			Debug.LogError("1");
			return;
		}
		AudioSource audioSource = base.gameObject.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
		}
		audioSource.clip = clip;
		audioSource.Play();
	}
}
