using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [HideInInspector] public bool isMute = false;

    [SerializeField] private AudioSource soundEffect; // Audio source for sound effects
    [SerializeField] private AudioSource soundMusic; // Audio source for background music
    [SerializeField] private Sound[] sounds; // Array of sound clips

    private float currentVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist SoundManager across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    private void Start()
    {
        PlayMusic(SoundType.BackgroundMusic); // Play background music
        currentVolume = soundMusic.volume;
    }

    public void MuteGame()
    {
        isMute = !isMute; // Toggle mute
        SetVolume(isMute ? 0.0f : currentVolume);
    }

    public void SetVolume(float newVolume)
    {
        currentVolume = newVolume == 0.0f ? soundMusic.volume : newVolume;
        soundEffect.volume = newVolume;
        soundMusic.volume = newVolume;
    }

    public void PlayMusic(SoundType soundType)
    {
        if (isMute) return;

        AudioClip soundClip = GetSoundClip(soundType);
        if (soundClip != null)
        {
            soundMusic.clip = soundClip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogWarning("Did not find any Sound Clip for selected Sound Type");
        }
    }

    public void PlayEffect(SoundType soundType)
    {
        if (isMute) return;

        AudioClip soundClip = GetSoundClip(soundType);
        if (soundClip != null)
        {
            soundEffect.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("Did not find any Sound Clip for selected Sound Type");
        }
    }

    private AudioClip GetSoundClip(SoundType soundType)
    {
        Sound sound = Array.Find(sounds, item => item.soundType == soundType);
        return sound?.soundClip;
    }
}

[Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip soundClip;
}

public enum SoundType
{
    ButtonClick,
    ButtonQuit,
    BackgroundMusic,
    PowerUpPickup,
    PlayerHeal,
    PlayerHurt,
    EnemyHurt,
    BulletShoot,
    GamePause,
    GameOver,
    GameStart
}