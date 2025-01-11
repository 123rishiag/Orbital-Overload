using ServiceLocator.Bullet;
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
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerConfig playerConfig;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private BulletService bulletService;
        private PlayerService playerService;

        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            soundService = new SoundService(soundConfig, sfxSource, bgSource);
            uiService = new UIService(uiCanvas, soundService);
            bulletService = new BulletService(bulletConfig, soundService);
            playerService = new PlayerService(playerConfig, soundService, uiService, player, bulletService);
        }
    }
}