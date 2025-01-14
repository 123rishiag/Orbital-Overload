using ServiceLocator.Actor;
using ServiceLocator.Bullet;
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

        [Header("Camera Components")]
        [SerializeField] private Camera mainCamera; // Main camera reference
        [SerializeField] private float cameraFollowSpeed; // Speed at which the camera follows the player

        [Header("Game Configs")]
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private ActorConfig actorConfig;
        [SerializeField] private PowerUpConfig powerUpConfig;

        // Private Variables
        private GameController gameController;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private BulletService bulletService;
        private ActorService actorService;
        private CameraService cameraService;
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
            cameraService = new CameraService(mainCamera, cameraFollowSpeed);
            bulletService = new BulletService(bulletConfig, soundService);
            actorService = new ActorService(actorConfig, soundService, bulletService);
            powerUpService = new PowerUpService(powerUpConfig, this, soundService, uiService, actorService);
        }

        private void InjectDependencies()
        {
            gameController.Init(soundService, uiService, cameraService, actorService);
        }

        private void Update()
        {
            gameController.Update();
            bulletService.Update();
            actorService.Update();
            powerUpService.Update();
        }

        private void FixedUpdate()
        {
            bulletService.FixedUpdate();
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

        public GameController GetGameController() => gameController;
    }
}