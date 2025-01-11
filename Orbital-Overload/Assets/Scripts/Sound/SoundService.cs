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

        public void MuteGame()
        {
            isMute = !isMute; // Toggle mute
            SetVolume(isMute ? 0.0f : sfxVolume, bgVolume);
        }

        public void SetVolume(float _sfxVolume, float _bgVolume)
        {
            sfxVolume = _sfxVolume == 0.0f ? sfxSource.volume : _sfxVolume;
            sfxSource.volume = _sfxVolume;

            bgVolume = _bgVolume == 0.0f ? bgSource.volume : _bgVolume;
            bgSource.volume = _bgVolume;
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
    }
}