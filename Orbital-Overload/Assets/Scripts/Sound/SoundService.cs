using ServiceLocator.Event;
using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundService
    {
        // Private Variables
        private SoundConfig soundConfig;
        private AudioSource sfxSource;
        private AudioSource bgSource;

        private bool isMute = false;
        private float sfxVolume;
        private float bgVolume;

        // Private Services
        private EventService eventService;

        public SoundService(SoundConfig _soundConfig, AudioSource _sfxSource, AudioSource _bgSource)
        {
            // Setting Variables
            soundConfig = _soundConfig;
            sfxSource = _sfxSource;
            bgSource = _bgSource;

            PlayBackgroundMusic(SoundType.BackgroundMusic, true);
            sfxVolume = sfxSource.volume;
            bgVolume = bgSource.volume;
        }

        public void Init(EventService _eventService)
        {
            // Setting Services
            eventService = _eventService;

            // Adding Listeners
            eventService.OnPlaySoundEffectEvent.AddListener(PlaySoundEffect);
        }

        public void Destroy()
        {
            // Removing Listeners
            eventService.OnPlaySoundEffectEvent.RemoveListener(PlaySoundEffect);
        }

        public void MuteGame()
        {
            isMute = !isMute; // Toggle mute
            SetVolume();
        }

        public void SetVolume()
        {
            sfxSource.volume = isMute ? 0.0f : sfxVolume;
            bgSource.volume = isMute ? 0.0f : bgVolume;
        }

        public void PlaySoundEffect(SoundType _soundType)
        {
            if (isMute) return;

            AudioClip clip = GetSoundClip(_soundType);
            if (clip != null)
            {
                sfxSource.clip = clip;
                sfxSource.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlayBackgroundMusic(SoundType _soundType, bool _loopSound = true)
        {
            if (isMute) return;

            AudioClip clip = GetSoundClip(_soundType);
            if (clip != null)
            {
                bgSource.loop = _loopSound;
                bgSource.clip = clip;
                bgSource.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType _soundType)
        {
            SoundData sound = Array.Find(soundConfig.soundList, item => item.soundType == _soundType);
            if (sound.soundClip != null)
                return sound.soundClip;
            return null;
        }

        // Getters
        public bool IsMute() => isMute;
    }
}