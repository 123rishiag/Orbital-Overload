using ServiceLocator.Actor;
using ServiceLocator.Control;
using ServiceLocator.PowerUp;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
using ServiceLocator.UI;
using ServiceLocator.Vision;
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
            InjectDependencies();

            // Setting Elements
            CreateStateMachine();
            gameStateMachine.ChangeState(GameState.Game_Start);
        }

        private void CreateStateMachine() => gameStateMachine = new GameStateMachine(this);
        private void CreateServices()
        {
            soundService = new SoundService(gameService.soundConfig, gameService.sfxSource, gameService.bgSource);
            uiService = new UIService(gameService.uiCanvas);
            inputService = new InputService();
            cameraService = new CameraService(gameService.mainCamera, gameService.cameraFollowSpeed);
            spawnService = new SpawnService();
            projectileService = new ProjectileService(gameService.projectileConfig, gameService.projectileParentPanel);
            powerUpService = new PowerUpService(gameService.powerUpConfig, gameService.powerUpParentPanel);
            actorService = new ActorService(gameService.actorConfig, gameService.actorParentPanel);
        }

        private void InjectDependencies()
        {
            uiService.Init(this);
            spawnService.Init(actorService);
            projectileService.Init(soundService, actorService);
            powerUpService.Init(gameService, soundService, uiService, spawnService);
            actorService.Init(soundService, uiService, inputService, spawnService, projectileService);
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

        // Getters
        public GameService GetGameService() => gameService;
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