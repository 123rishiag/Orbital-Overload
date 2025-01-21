using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        // Private Variables
        private SoundController soundController;

        public void Init(SoundController _soundController)
        {
            soundController = _soundController;
            Reset();
        }

        public void Reset()
        {
            audioSource.clip = soundController.GetSoundModel().SoundClip;
            audioSource.volume = soundController.GetSoundModel().SoundVolume;
            audioSource.loop = soundController.GetSoundModel().IsSoundLoop;
        }

        public void PlaySound() => audioSource.Play();

        public void SetVolume(bool _isMute)
        {
            audioSource.volume = _isMute ? 0.0f : soundController.GetSoundModel().SoundVolume;
        }

        public bool IsSoundPlaying() => audioSource.isPlaying;

        public void ShowView() => gameObject.SetActive(true);
        public void HideView() => gameObject.SetActive(false);
    }
}
