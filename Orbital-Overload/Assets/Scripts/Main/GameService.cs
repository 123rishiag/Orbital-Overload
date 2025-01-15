using ServiceLocator.Actor;
using ServiceLocator.PowerUp;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
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

        [Header("Camera Components")]
        [SerializeField] private Camera mainCamera; // Main camera reference
        [SerializeField] private float cameraFollowSpeed; // Speed at which the camera follows the player

        [Header("Game Configs")]
        [SerializeField] private ProjectileConfig projectileConfig;
        [SerializeField] private PowerUpConfig powerUpConfig;
        [SerializeField] private ActorConfig actorConfig;

        // Private Variables
        private GameController gameController;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private CameraService cameraService;
        private SpawnService spawnService;
        private ProjectileService projectileService;
        private PowerUpService powerUpService;
        private ActorService actorService;

        private void Start()
        {
            CreateServicesAndControllers();
            InjectDependencies();
        }

        private void CreateServicesAndControllers()
        {
            gameController = new GameController();
            soundService = new SoundService(soundConfig, sfxSource, bgSource);
            uiService = new UIService(uiCanvas);
            cameraService = new CameraService(mainCamera, cameraFollowSpeed);
            spawnService = new SpawnService();
            projectileService = new ProjectileService(projectileConfig);
            powerUpService = new PowerUpService(powerUpConfig);
            actorService = new ActorService(actorConfig);
        }

        private void InjectDependencies()
        {
            gameController.Init(soundService, uiService, cameraService, actorService);
            uiService.Init(this, actorService);
            spawnService.Init(actorService);
            projectileService.Init(soundService, actorService);
            powerUpService.Init(this, soundService, uiService, actorService);
            actorService.Init(soundService, spawnService, projectileService);
        }

        private void Update()
        {
            gameController.Update();
            uiService.Update();
            spawnService.Update();
            projectileService.Update();
            powerUpService.Update();
            actorService.Update();
        }

        private void FixedUpdate()
        {
            projectileService.FixedUpdate();
            actorService.FixedUpdate();
        }

        private void LateUpdate()
        {
            gameController.LateUpdate();
        }

        public void StartManagedCoroutine(IEnumerator _coroutine)
        {
            StartCoroutine(_coroutine);
        }

        // Getters
        public GameController GetGameController() => gameController;
    }
}