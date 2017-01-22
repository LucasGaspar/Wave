using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	//Sounds
	List<AudioClip> currentSoundClips;
	const float cooldownForSounds = 0.05f;

	//Music
	AudioClip nextMusic;
	bool loopMusic = true;

	//Channels
	AudioSource musicChannel;
	AudioSource musicChannelCrossfadeHelper;
	AudioSource soundChannel;
	
	//General
	float maxMusicVolume = 1f;
	float maxSoundVolume = 0.7f;
	float musicVolume = 1f;
	float soundVolume = 1f;
	
	//Static Reference
	static AudioManager instance;
	static AudioManager Instance
	{
		get
		{
			if (instance == null)
				new GameObject ("AudioManager").AddComponent<AudioManager> ();
			return instance;
		}
		set
		{
			instance = value;
		}
	}

	public static bool MusicMuted
	{
		get { return Instance.musicChannel.mute; }
	}

	public static bool SoundMuted
	{
		get { return Instance.soundChannel.mute; }
	}

	public static float MaxMusicVolume
	{
		get { return Instance.maxMusicVolume; }
	}

	public static float MaxSoundVolume
	{
		get { return Instance.maxSoundVolume; }
	}

	float MusicChannelVolume
	{
		get{ return musicVolume * maxMusicVolume; }
	}

	float SoundChannelVolume
	{
		get{ return soundVolume * maxSoundVolume; }
	}
	
	#region staticFunctions

	public static void SetMaxMusicVolume(float volume)
	{
		Instance.SetMaxMusicVolumeInternal(volume);
	}

	public static void SetMaxSoundVolume(float volume)
	{
		Instance.SetMaxSoundVolumeInternal(volume);
	}

	public static void SetMusicVolume(float volume, bool fade = true)
	{
		Instance.SetMusicVolumeInternal(volume, fade);
	}

	public static void SetSoundVolume(float volume)
	{
		Instance.SetSoundVolumeInternal(volume);
	}

	public static void ReproduceSound(AudioClip clip)
	{
		Instance.ReproduceSoundInternal(clip);
	}

	public static void ReproduceMusic(AudioClip clip, bool loop = true)
	{
		Instance.ReproduceMusicInternal(clip, loop);
	}

	public static void CrossFadeMusic(AudioClip clip, bool loop = true)
	{
		Instance.CrossFadeMusicInternal(clip, loop);
	}

	public static void StopMusic()
	{
		Instance.StopMusicInternal();
	}

	public static void SetNextMusic(AudioClip clip)
	{
		Instance.SetNextMusicInternal(clip);
	}

	public static void SetMusicMute (bool state)
	{
		Instance.SetMusicMuteInternal (state);
	}

	public static void SetSoundMute (bool state)
	{
		Instance.SetSoundMuteInternal (state);
	}
	#endregion
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
			Init();
		}
		else
		{
			DestroyImmediate (this.gameObject);
		}
	}
	
	void Init()
	{
		//Music
		musicChannel = gameObject.AddComponent<AudioSource> ();
		musicChannel.loop = false;
		musicChannelCrossfadeHelper = gameObject.AddComponent<AudioSource> ();
		musicChannelCrossfadeHelper.loop = true;
		
		//Sounds
		soundChannel = gameObject.AddComponent<AudioSource> ();
		currentSoundClips = new List<AudioClip> ();

		//Volume
		maxMusicVolume = PlayerPrefs.GetFloat ("maxMusicVolume", 1.0f);
		maxSoundVolume = PlayerPrefs.GetFloat ("maxSoundVolume", 0.8f);
		SetMusicVolumeInternal (musicVolume);
		SetSoundVolumeInternal (soundVolume);
	}

	void Update()
	{
		//Loop for music or add next music
		if (!musicChannel.isPlaying)
		{
			if (nextMusic != null)
			{
				musicChannel.clip = nextMusic;
				musicChannel.Play ();
				nextMusic = null;
			}
			else if (musicChannel.clip != null && loopMusic)
			{
				musicChannel.Play ();
			}
		}
		//Adjust volume smoothly
		if (musicChannel.volume < MusicChannelVolume)
		{
			musicChannel.volume += Time.unscaledDeltaTime;
			if (musicChannel.volume > MusicChannelVolume)
				musicChannel.volume = MusicChannelVolume;
		}
		else if (musicChannel.volume > MusicChannelVolume)
		{
			musicChannel.volume -= Time.unscaledDeltaTime;
			if (musicChannel.volume < MusicChannelVolume)
				musicChannel.volume = MusicChannelVolume;
		}
		//Crossfade effect
		if (musicChannelCrossfadeHelper.volume > 0)
		{
			musicChannelCrossfadeHelper.volume -= Time.smoothDeltaTime/2;
			if (musicChannelCrossfadeHelper.volume <= 0)
			{
				musicChannelCrossfadeHelper.volume = 0;
				musicChannelCrossfadeHelper.Stop ();
			}
		}
	}

	float InternalGetMaxMusicVolume ()
	{
		return maxMusicVolume;
	}

	float InternalGetMaxSoundVolume ()
	{
		return maxSoundVolume;
	}

	void SetMaxMusicVolumeInternal(float volume)
	{
		maxMusicVolume = volume;
		PlayerPrefs.SetFloat ("maxMusicVolume", maxMusicVolume);

		SetMusicVolumeInternal (musicVolume, false);
	}

	void SetMaxSoundVolumeInternal(float volume)
	{
		maxSoundVolume = volume;
		PlayerPrefs.SetFloat ("maxSoundVolume", maxSoundVolume);
		
		SetSoundVolumeInternal (soundVolume);
	}

	void SetMusicVolumeInternal(float volume, bool fade = true)
	{
		musicVolume = volume;
		if (!fade)
			musicChannel.volume = MusicChannelVolume;
	}
	
	void SetSoundVolumeInternal(float volume)
	{
		soundVolume = volume;
		soundChannel.volume = SoundChannelVolume;
	}

	void ReproduceSoundInternal(AudioClip clip)
	{
		if(!SoundMuted && !currentSoundClips.Contains(clip))
		{
			soundChannel.PlayOneShot(clip);
			currentSoundClips.Add(clip);
			Invoke("RemoveSoundCooldown", cooldownForSounds);
		}
	}

	void ReproduceMusicInternal(AudioClip clip, bool loop)
	{
		musicChannel.clip = clip;
		musicChannel.Play ();
		this.loopMusic = loop;
	}

	void CrossFadeMusicInternal(AudioClip clip, bool loop)
	{
		if (musicChannel.isPlaying)
		{
			musicChannelCrossfadeHelper.clip = musicChannel.clip;
			musicChannelCrossfadeHelper.time = musicChannel.time;
			musicChannelCrossfadeHelper.volume = musicChannel.volume;
			musicChannelCrossfadeHelper.Play ();
		}
		ReproduceMusicInternal (clip, loop);
		musicChannel.volume = 0;
	}

	void StopMusicInternal()
	{
		musicChannel.clip = null;
		musicChannel.Stop ();
	}

	void SetNextMusicInternal(AudioClip clip)
	{
		nextMusic = clip;
	}

	public void ReproducePersistentSoundInternal(AudioClip clip, float duration)
	{
		if(soundChannel.isPlaying)
			soundChannel.Stop ();
		soundChannel.clip = clip;
		soundChannel.Play ();
		CancelInvoke("StopPersistentSoundInternal");
		Invoke("StopPersistentSoundInternal", duration);
	}
	
	public void StopPersistentSoundInternal()
	{
		soundChannel.Stop();
		soundChannel.clip = null;
	}

	void RemoveSoundCooldown()
	{
		currentSoundClips.RemoveAt(currentSoundClips.Count-1);
	}

	// /////////////////////// //
	// ////////  MUTE  /////// //
	// /////////////////////// //

	void SetMusicMuteInternal (bool state)
	{
		musicChannel.mute = musicChannelCrossfadeHelper.mute = state;
	}

	void SetSoundMuteInternal (bool state)
	{
		soundChannel.mute = state;
	}

	// ///////////////////////// //
	// ////// ENUMERATORS ////// //
	// ///////////////////////// //

	/// <summary> Cointains the different types of audio. </summary>
	public enum AudioType
	{
		Sound,
		Music
	}
}