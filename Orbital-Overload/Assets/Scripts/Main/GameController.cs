using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator.Main
{
    public class GameController
    {
        // Private Variables
        private bool isPaused; // Whether the game is paused
        private bool canPause; // Whether the game can be paused

        // Private Services
        private SoundService soundService;
        private UIService uiService;

        public GameController()
        {
            // Setting Variables
            isPaused = false;
            canPause = true;
        }

        public void Init(SoundService _soundService, UIService _uiService)
        {
            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
        }

        public void PauseGame()
        {
            if (canPause)
            {
                Time.timeScale = 0f; // Pause the game
                uiService.GetUIController().GetUIView().pauseMenuPanel.SetActive(true); // Show Pause Menu
                isPaused = true;
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
            canPause = false; // Disable pausing
            Time.timeScale = 0f; // Stop time
            uiService.GetUIController().GetUIView().gameOverMenuPanel.SetActive(true); // Show Game Over Menu
            soundService.PlaySoundEffect(SoundType.GameOver); // Play game over sound effect
        }

        public void RestartGame()
        {
            canPause = true;
            Time.timeScale = 1f; // Restart time
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
            soundService.PlaySoundEffect(SoundType.GameStart); // Play game start sound effect
        }

        public void MainMenu()
        {
            uiService.GetUIController().GetUIView().mainMenuPanel.SetActive(true); // Show Main Menu
            uiService.GetUIController().GetUIView().pauseMenuPanel.SetActive(false); // Hide Pause Menu
            uiService.GetUIController().GetUIView().gameOverMenuPanel.SetActive(false); // Hide Game Over Menu
            soundService.PlaySoundEffect(SoundType.ButtonQuit); // Play quit sound effect
        }

        public void PlayGame()
        {
            Time.timeScale = 1f; // Ensure game time is running
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