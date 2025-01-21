using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundService
    {
        // Private Variables
        private SoundConfig soundConfig;
        private Transform soundParentPanel;

        private SoundPool soundPool;

        private bool isMute = false;

        // Private Services
        private EventService eventService;

        public SoundService(SoundConfig _soundConfig, Transform _soundParentPanel)
        {
            // Setting Variables
            soundConfig = _soundConfig;
            soundParentPanel = _soundParentPanel;

            // Creating Object Pool for sound
            soundPool = new SoundPool(soundConfig, soundParentPanel);
        }

        public void Init(EventService _eventService)
        {
            // Setting Services
            eventService = _eventService;

            // Adding Listeners
            eventService.OnPlaySoundEvent.AddListener(PlaySound);

            // Setting Elements
            PlaySound(SoundType.BackgroundMusic);
        }

        public void Update()
        {
            ProcessSoundUpdate();
        }

        private void ProcessSoundUpdate()
        {
            // For Sounds
            for (int i = soundPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!soundPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var soundController = soundPool.pooledItems[i].Item;
                if (!soundController.IsActive()) ReturnSoundToPool(soundController);
            }
        }

        public void Reset()
        {
            // Disabling All Sounds
            for (int i = soundPool.pooledItems.Count - 1; i >= 0; i--)
            {
                var soundController = soundPool.pooledItems[i].Item;
                ReturnSoundToPool(soundController);
            }

            // Playing Background Music
            PlaySound(SoundType.BackgroundMusic);
        }

        public void Destroy()
        {
            // Removing Listeners
            eventService.OnPlaySoundEvent.RemoveListener(PlaySound);
        }

        public void MuteGame()
        {
            // Toggle mute
            isMute = !isMute;
            for (int i = soundPool.pooledItems.Count - 1; i >= 0; i--)
            {
                var soundController = soundPool.pooledItems[i].Item;
                soundController.SetMute(isMute);
            }
        }

        public void PlaySound(SoundType _soundType)
        {
            if (isMute) return;

            float volume = _soundType == SoundType.BackgroundMusic ? soundConfig.bgVolume : soundConfig.soundVolume;
            bool isLoop = _soundType == SoundType.BackgroundMusic ? true : false;
            soundPool.GetItem<SoundController>(volume, isLoop, _soundType);
        }

        private void ReturnSoundToPool(SoundController _soundToReturn)
        {
            _soundToReturn.GetSoundView().HideView();
            soundPool.ReturnItem(_soundToReturn);
        }

        // Getters
        public bool IsMute() => isMute;
    }
}