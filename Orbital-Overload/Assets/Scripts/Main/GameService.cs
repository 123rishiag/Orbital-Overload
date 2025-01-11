using ServiceLocator.Player;
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
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerConfig playerConfig;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private PlayerService playerService;

        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            soundService = new SoundService(soundConfig, sfxSource, bgSource);
            uiService = new UIService(uiCanvas, soundService);
            playerService = new PlayerService(playerConfig, soundService, uiService, player);
        }
    }
}