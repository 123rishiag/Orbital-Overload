using ServiceLocator.Actor;
using ServiceLocator.Control;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Vision;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator.Main
{
    public class GameController
    {
        // Private Variables
        private bool isPaused; // Whether the game is paused
        private bool canPause; // Whether the game can be paused
        private bool isGameOver; // Whether the game is over
        private bool canGameOver;// Whether the game can be over

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private InputService inputService;
        private CameraService cameraService;
        private ActorService actorService;

        public GameController()
        {
            // Setting Variables
            isPaused = true;
            canPause = false;
            isGameOver = false;
            canGameOver = true;
            Time.timeScale = 0f;
        }

        public void Init(SoundService _soundService, UIService _uiService, InputService _inputService,
            CameraService _cameraService, ActorService _actorService)
        {
            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
            inputService = _inputService;
            cameraService = _cameraService;
            actorService = _actorService;
        }
        public void Update()
        {
            if (inputService.IsEscapePressed)
            {
                if (isPaused)
                {
                    ResumeGame(); // Resume game if paused
                }
                else
                {
                    PauseGame(); // Pause game if not paused
                }
            }

            if ((!actorService.GetPlayerActorController().IsAlive()))
            {
                GameOver();
            }
        }

        public void LateUpdate()
        {
            // Camera should follow player
            cameraService.FollowCameraTowardsPosition(
                actorService.GetPlayerActorController().GetActorView().GetPosition());
        }

        public void PauseGame()
        {
            if (canPause && !isGameOver)
            {
                Time.timeScale = 0f; // Pause the game
                isPaused = true;
                uiService.GetUIController().GetUIView().pauseMenuPanel.SetActive(true); // Show Pause Menu
                soundService.PlaySoundEffect(SoundType.GamePause); // Play pause sound effect
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f; // Resume the game
            uiService.GetUIController().GetUIView().pauseMenuPanel.SetActive(false); // Hide Pause Menu
            isPaused = false;
            soundService.PlaySoundEffect(SoundType.GamePause); // Play resume sound effect
        }

        public void GameOver()
        {
            if (canGameOver)
            {
                canPause = false; // Disable pausing
                canGameOver = false; // Disable game Over
                isGameOver = true;
                Time.timeScale = 0f; // Stop time
                uiService.GetUIController().GetUIView().gameOverMenuPanel.SetActive(true); // Show Game Over Menu
                soundService.PlaySoundEffect(SoundType.GameOver); // Play game over sound effect
            }
        }

        public void RestartGame()
        {
            canPause = true;
            canGameOver = true;
            Time.timeScale = 1f; // Restart time
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
            uiService.GetUIController().GetUIView().mainMenuPanel.SetActive(false); // Hide Main Menu
            uiService.GetUIController().GetUIView().pauseMenuPanel.SetActive(false); // Hide Pause Menu
            uiService.GetUIController().GetUIView().gameOverMenuPanel.SetActive(false); // Hide Game Over Menu
            soundService.PlaySoundEffect(SoundType.GameStart); // Play game start sound effect
        }

        public void MainMenu()
        {
            canPause = true;
            canGameOver = true;
            SceneManager.LoadScene(0); // Reload 0th scene
            uiService.GetUIController().GetUIView().mainMenuPanel.SetActive(true); // Show Main Menu
            uiService.GetUIController().GetUIView().pauseMenuPanel.SetActive(false); // Hide Pause Menu
            uiService.GetUIController().GetUIView().gameOverMenuPanel.SetActive(false); // Hide Game Over Menu
            soundService.PlaySoundEffect(SoundType.ButtonQuit); // Play quit sound effect
        }

        public void PlayGame()
        {
            Time.timeScale = 1f; // Ensure game time is running
            isPaused = false;
            canPause = true;
            uiService.GetUIController().GetUIView().mainMenuPanel.SetActive(false); // Hide Main Menu
            soundService.PlaySoundEffect(SoundType.GameStart); // Play game start sound effect
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MuteGame()
        {
            bool isMute = uiService.GetUIController().GetUIView().mainMenuMuteButtonText.text == "Mute: On";
            uiService.GetUIController().GetUIView().mainMenuMuteButtonText.text = isMute ? "Mute: Off" : "Mute: On"; // Toggle mute text
            soundService.MuteGame(); // Mute/unmute the game
            soundService.PlaySoundEffect(SoundType.ButtonClick); // Play button click sound effect
        }
    }
}