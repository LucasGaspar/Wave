using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
	public AudioClip music;
	void Start ()
	{
		Invoke("IncreaseVolume", 4.75f);
		AudioManager.ReproduceMusic(null);
		AudioManager.SetMusicVolume(0.5f);
	}

	void IncreaseVolume()
	{
		AudioManager.ReproduceMusic(music);
		AudioManager.SetMusicVolume(1f);
	}
}
