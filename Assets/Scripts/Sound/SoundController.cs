using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundController
    {
        // Private Variables
        private SoundModel soundModel;
        private SoundView soundView;

        public SoundController(SoundData _soundData, SoundView _soundPrefab,
            Transform _soundParentPanel, float _soundVolume, bool _isLoop)
        {
            // Setting Variables
            soundModel = new SoundModel(_soundData, _soundVolume, _isLoop);
            soundView = Object.Instantiate(_soundPrefab, _soundParentPanel).
                GetComponent<SoundView>();
            soundView.Init(this);
        }

        public void Reset(SoundData _soundData, float _soundVolume, bool _isLoop)
        {
            soundModel.Reset(_soundData, _soundVolume, _isLoop);
            soundView.Reset();
            soundView.ShowView();
            soundView.PlaySound();
        }

        public bool IsActive()
        {
            if (!soundView.gameObject.activeInHierarchy) return false;
            if (!soundView.IsSoundPlaying() && !soundModel.IsSoundLoop) return false;
            return true;
        }

        public void SetMute(bool _isMute) => soundView.SetVolume(_isMute);

        // Getters
        public SoundModel GetSoundModel() => soundModel;
        public SoundView GetSoundView() => soundView;
    }
}