using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Private Variables
        [Header("Core Components")]
        [SerializeField] private SoundConfig soundConfig;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgSource;
        [SerializeField] private UIController uiCanvas;

        // Private Services
        private SoundService soundService;
        private UIService uiService;


        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            soundService = new SoundService(soundConfig, sfxSource, bgSource);
            uiService = new UIService(uiCanvas, soundService);
        }
    }
}