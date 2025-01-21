using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundModel
    {
        public SoundModel(SoundData _soundData, float _soundVolume, bool _isLoop)
        {
            Reset(_soundData, _soundVolume, _isLoop);
        }

        public void Reset(SoundData _soundData, float _soundVolume, bool _isLoop)
        {
            SoundType = _soundData.soundType;
            SoundClip = _soundData.soundClip;
            SoundVolume = _soundVolume;
            IsSoundLoop = _isLoop;
        }

        // Getters & Setters
        public SoundType SoundType { get; private set; }
        public AudioClip SoundClip { get; private set; }
        public float SoundVolume { get; private set; }
        public bool IsSoundLoop { get; private set; }
    }
}
