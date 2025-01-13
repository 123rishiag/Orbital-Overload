using ServiceLocator.Bullet;
using ServiceLocator.Enemy;
using ServiceLocator.Player;
using ServiceLocator.PowerUp;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Vision;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Inspector Variables
        [Header("Sound Components")]
        [SerializeField] private SoundConfig soundConfig;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgSource;

        [Header("UI Components")]
        [SerializeField] private UIView uiCanvas;

        [Header("Game Configs")]
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private PowerUpConfig powerUpConfig;

        [Header("Camera Components")]
        [SerializeField] private Camera mainCamera; // Main camera reference
        [SerializeField] private float cameraFollowSpeed; // Speed at which the camera follows the player

        // Private Variables
        private GameController gameController;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private BulletService bulletService;
        private PlayerService playerService;
        private CameraService cameraService;
        private EnemyService enemyService;
        private PowerUpService powerUpService;

        private void Start()
        {
            CreateServicesAndControllers();
            InjectDependencies();
        }

        private void CreateServicesAndControllers()
        {
            gameController = new GameController();
            soundService = new SoundService(soundConfig, sfxSource, bgSource);
            uiService = new UIService(uiCanvas, this);
            bulletService = new BulletService(bulletConfig, soundService);
            playerService = new PlayerService(playerConfig, this, soundService, uiService, bulletService);
            cameraService = new CameraService(mainCamera, cameraFollowSpeed, playerService);
            enemyService = new EnemyService(enemyConfig, bulletService, playerService);
            powerUpService = new PowerUpService(powerUpConfig, this, soundService, uiService, playerService);
        }

        private void InjectDependencies()
        {
            gameController.Init(soundService, uiService);
        }

        private void Update()
        {
            gameController.Update();
            bulletService.Update();
            playerService.Update();
            enemyService.Update();
            powerUpService.Update();
        }

        private void FixedUpdate()
        {
            bulletService.FixedUpdate();
            playerService.FixedUpdate();
            enemyService.FixedUpdate();
        }

        private void LateUpdate()
        {
            cameraService.LateUpdate();
        }

        public void StartManagedCoroutine(IEnumerator _coroutine)
        {
            StartCoroutine(_coroutine);
        }

        public GameController GetGameController() => gameController;
    }
}