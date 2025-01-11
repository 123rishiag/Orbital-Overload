using ServiceLocator.Bullet;
using ServiceLocator.Enemy;
using ServiceLocator.Player;
using ServiceLocator.PowerUp;
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
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private PowerUpConfig powerUpConfig;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private BulletService bulletService;
        private PlayerService playerService;
        private EnemyService enemyService;
        private PowerUpService powerUpService;

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
            enemyService = new EnemyService(enemyConfig, bulletService, playerService);
            powerUpService = new PowerUpService(powerUpConfig, soundService, uiService, playerService);
        }

        private void Update()
        {
            enemyService.Update();
            powerUpService.Update();
        }
    }
}