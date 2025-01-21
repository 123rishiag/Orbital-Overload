using ServiceLocator.Utility;
using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundPool : GenericObjectPool<SoundController>
    {
        // Private Variables
        private SoundConfig soundConfig;
        private Transform soundParentPanel;

        private float soundVolume;
        private bool isLoop;
        private SoundType soundType;

        public SoundPool(SoundConfig _soundConfig, Transform _soundParentPanel)
        {
            // Setting Variables
            soundConfig = _soundConfig;
            soundParentPanel = _soundParentPanel;
        }

        public SoundController GetItem<T>(float _soundVolume, bool _isLoop, SoundType _soundType) where T : SoundController
        {
            // Setting Variables
            soundVolume = _soundVolume;
            isLoop = _isLoop;
            soundType = _soundType;

            // Fetching Item
            var item = GetItem<T>();

            // Fetching Index
            int soundIndex = GetSoundIndex();

            // Resetting Item Properties
            item.Reset(soundConfig.soundData[soundIndex], soundVolume, isLoop);

            return item;
        }

        protected override SoundController CreateItem<T>()
        {
            // Fetching Index
            int soundIndex = GetSoundIndex();

            // Creating Controller
            return new SoundController(soundConfig.soundData[soundIndex], soundConfig.soundPrefab,
                soundParentPanel, soundVolume, isLoop);
        }

        private int GetSoundIndex()
        {
            // Fetching Index
            return Array.FindIndex(soundConfig.soundData, data => data.soundType == soundType);
        }
    }
}
