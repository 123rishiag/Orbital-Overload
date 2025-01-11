using ServiceLocator.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text healthText; // Text for displaying health
        [SerializeField] private TMP_Text scoreText; // Text for displaying score
        [SerializeField] private GameObject powerUpBar; // Power Up Bar
        [SerializeField] private TMP_Text powerUpText; // Text for displaying power-up status

        [Header("Main Menu Elements")]
        [SerializeField] private GameObject mainMenuPanel; // Main Menu Panel
        [SerializeField] private Button mainMenuPlayButton; // Button to start the game
        [SerializeField] private Button mainMenuQuitButton; // Button to quit the game
        [SerializeField] private Button mainMenuMuteButton; // Button to mute/unmute the game
        [SerializeField] private TMP_Text mainMenuMuteButtonText; // Text of mute button

        [Header("Pause Menu Elements")]
        [SerializeField] private GameObject pauseMenuPanel; // Pause Menu Panel
        [SerializeField] private Button pauseMenuResumeButton; // Button to resume game
        [SerializeField] private Button pauseMenuMainMenuButton; // Button to go to main menu

        [Header("Game Over Menu Elements")]
        [SerializeField] private GameObject gameOverMenuPanel; // Game Over Menu Panel
        [SerializeField] private Button gameOverMenuRestartButton; // Button to restart game
        [SerializeField] private Button gameOverMenuMainMenuButton; // Another button to go to main menu

        // Private Variables
        private bool isPaused = false; // Whether the game is paused
        private bool canPause = true; // Whether the game can be paused

        // Private Services
        private SoundService soundService;

        public void Init(SoundService _soundService)
        {
            soundService = _soundService;
        }

        private void Start()
        {
            mainMenuPlayButton.onClick.AddListener(PlayGame); // Add listener to play button
            mainMenuQuitButton.onClick.AddListener(QuitGame); // Add listener to quit button
            mainMenuMuteButton.onClick.AddListener(MuteGame); // Add listener to mute button

            pauseMenuResumeButton.onClick.AddListener(ResumeGame); // Add listener to resume button
            pauseMenuMainMenuButton.onClick.AddListener(MainMenu); // Add listener to main menu button

            gameOverMenuRestartButton.onClick.AddListener(RestartGame); // Add listener to restart button
            gameOverMenuMainMenuButton.onClick.AddListener(MainMenu); // Add listener to another main menu button
        }

        private void Update()
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
                pauseMenuPanel.SetActive(true); // Show Pause Menu
                isPaused = true;
                soundService.PlaySoundEffect(SoundType.GamePause); // Play pause sound effect
            }
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f; // Resume the game
            pauseMenuPanel.SetActive(false); // Hide Pause Menu
            isPaused = false;
            soundService.PlaySoundEffect(SoundType.GamePause); // Play resume sound effect
        }

        public void GameOver()
        {
            canPause = false; // Disable pausing
            Time.timeScale = 0f; // Stop time
            gameOverMenuPanel.SetActive(true); // Show Game Over Menu
            soundService.PlaySoundEffect(SoundType.GameOver); // Play game over sound effect
        }

        private void RestartGame()
        {
            canPause = true;
            Time.timeScale = 1f; // Restart time
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
            soundService.PlaySoundEffect(SoundType.GameStart); // Play game start sound effect
        }

        private void MainMenu()
        {
            mainMenuPanel.SetActive(true); // Show Main Menu
            pauseMenuPanel.SetActive(false); // Hide Pause Menu
            gameOverMenuPanel.SetActive(false); // Hide Game Over Menu
            soundService.PlaySoundEffect(SoundType.ButtonQuit); // Play quit sound effect
        }

        public void PlayGame()
        {
            Time.timeScale = 1f; // Ensure game time is running
            mainMenuPanel.SetActive(false); // Hide Main Menu
            soundService.PlaySoundEffect(SoundType.GameStart); // Play game start sound effect
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        }

        private void MuteGame()
        {
            bool isMute = mainMenuMuteButtonText.text == "Mute: On";
            mainMenuMuteButtonText.text = isMute ? "Mute: Off" : "Mute: On"; // Toggle mute text
            soundService.MuteGame(); // Mute/unmute the game
            soundService.PlaySoundEffect(SoundType.ButtonClick); // Play button click sound effect
        }

        public void UpdateScoreText(int _score)
        {
            scoreText.text = "Score: " + _score;
        }

        public void UpdatePowerUpText(string _text)
        {
            powerUpBar.SetActive(true);
            powerUpText.text = _text; // Display power-up text
        }

        public void HidePowerUpText()
        {
            powerUpBar.SetActive(false);
        }

        public void UpdateHealthText(int _health)
        {
            healthText.text = "Health: " + _health; // Display health
        }
    }
}