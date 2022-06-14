using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///using NaughtyAttributes;

public class RandomSounds : MonoBehaviour
{
	public AudioClip previous;
	public MuteButton heck;
	private void Update()
	{
		if (UnityEngine.Random.value <= ChancePerSecond * Time.deltaTime && !AudioSource.isPlaying && heck.toggle)
		{
			if (this.ChooseRandomClip)
			{
				AudioSource.clip = Clips[UnityEngine.Random.Range(1, Clips.Length)];
			}
			if (!previous.Equals(AudioSource.clip))
            {
				AudioSource.Play();
				previous = AudioSource.clip;
			}
		}
	}

	public AudioSource AudioSource;

	public float ChancePerSecond = 0.5f;

	public bool ChooseRandomClip;

	public AudioClip[] Clips;
}
