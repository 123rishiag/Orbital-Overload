using ServiceLocator.Actor;
using ServiceLocator.Control;
using ServiceLocator.Event;
using ServiceLocator.PowerUp;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
using ServiceLocator.UI;
using ServiceLocator.VFX;
using ServiceLocator.Vision;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator.Main
{
    public class GameController
    {
        // Private Variables
        private GameStateMachine gameStateMachine;

        // Services
        private GameService gameService;
        private EventService eventService;
        private VFXService vfxService;
        private SoundService soundService;
        private UIService uiService;
        private InputService inputService;
        private CameraService cameraService;
        private SpawnService spawnService;
        private ProjectileService projectileService;
        private PowerUpService powerUpService;
        private ActorService actorService;

        public GameController(GameService _gameService)
        {
            // Setting Services
            gameService = _gameService;
            CreateServices();

            // Adding Listeners
            eventService.OnGetGameControllerEvent.AddListener(GetGameController);

            // Setting Elements
            InjectDependencies();
            CreateStateMachine();
            gameStateMachine.ChangeState(GameState.Game_Start);
        }

        public void Destroy()
        {
            // Calling Service's Destroy
            vfxService.Destroy();
            soundService.Destroy();
            uiService.Destroy();
            cameraService.Destroy();

            // Removing Listeners
            eventService.OnGetGameControllerEvent.RemoveListener(GetGameController);
        }

        private void CreateStateMachine() => gameStateMachine = new GameStateMachine(this);
        private void CreateServices()
        {
            eventService = new EventService();
            vfxService = new VFXService(gameService.vfxConfig, gameService.vfxParentPanel);
            soundService = new SoundService(gameService.soundConfig, gameService.sfxSource, gameService.bgSource);
            uiService = new UIService(gameService.uiCanvas);
            inputService = new InputService();
            cameraService = new CameraService(gameService.cameraConfig, gameService.mainCamera);
            spawnService = new SpawnService();
            projectileService = new ProjectileService(gameService.projectileConfig, gameService.projectileParentPanel);
            powerUpService = new PowerUpService(gameService.powerUpConfig, gameService.powerUpParentPanel);
            actorService = new ActorService(gameService.actorConfig, gameService.actorParentPanel);
        }

        private void InjectDependencies()
        {
            vfxService.Init(eventService);
            soundService.Init(eventService);
            uiService.Init(eventService);
            cameraService.Init(eventService);
            spawnService.Init(actorService);
            projectileService.Init(eventService, actorService);
            powerUpService.Init(eventService, spawnService);
            actorService.Init(eventService, inputService, spawnService, projectileService);
        }

        public void Reset()
        {
            vfxService.Reset();
            uiService.Reset();
            cameraService.Reset();
            spawnService.Reset();
            projectileService.Reset();
            powerUpService.Reset();
            actorService.Reset();
        }

        public void Update()
        {
            gameStateMachine.Update();
        }
        public void FixedUpdate()
        {
            gameStateMachine.FixedUpdate();
        }
        public void LateUpdate()
        {
            gameStateMachine.LateUpdate();
        }
        public void PlayGame()
        {
            soundService.PlaySoundEffect(SoundType.ButtonClick);
            gameStateMachine.ChangeState(GameState.Game_Play);
        }
        public void RestartGame()
        {
            soundService.PlaySoundEffect(SoundType.ButtonClick);
            gameStateMachine.ChangeState(GameState.Game_Restart);
        }
        public void MainMenu()
        {
            soundService.PlaySoundEffect(SoundType.ButtonQuit);
            SceneManager.LoadScene(0); // Reload 0th scene
        }

        public void QuitGame()
        {
            soundService.PlaySoundEffect(SoundType.ButtonQuit);
            Application.Quit();
        }

        public void MuteGame()
        {
            soundService.PlaySoundEffect(SoundType.ButtonClick);
            uiService.GetUIController().GetUIView().SetMuteButtonText(soundService.IsMute());
            soundService.MuteGame(); // Mute/unmute the game
            soundService.PlaySoundEffect(SoundType.ButtonClick); // Play button click sound effect
        }

        public Coroutine StartManagedCoroutine(IEnumerator _coroutine)
        {
            return gameService.StartCoroutine(_coroutine);
        }

        public void StopManagedCoroutine(Coroutine _coroutine)
        {
            gameService.StopCoroutine(_coroutine);
        }

        // Getters
        public GameController GetGameController() => this;
        public GameService GetGameService() => gameService;
        public EventService GetEventService() => eventService;
        public VFXService GetVFXService() => vfxService;
        public SoundService GetSoundService() => soundService;
        public UIService GetUIService() => uiService;
        public InputService GetInputService() => inputService;
        public CameraService GetCameraService() => cameraService;
        public SpawnService GetSpawnService() => spawnService;
        public ProjectileService GetProjectileService() => projectileService;
        public PowerUpService GetPowerUpService() => powerUpService;
        public ActorService GetActorService() => actorService;
    }
}