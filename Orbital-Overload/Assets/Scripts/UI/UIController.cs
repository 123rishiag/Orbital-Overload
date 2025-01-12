using ServiceLocator.Main;
using TMPro;
using UnityEngine;
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
        [SerializeField] public GameObject mainMenuPanel; // Main Menu Panel
        [SerializeField] private Button mainMenuPlayButton; // Button to start the game
        [SerializeField] private Button mainMenuQuitButton; // Button to quit the game
        [SerializeField] private Button mainMenuMuteButton; // Button to mute/unmute the game
        [SerializeField] public TMP_Text mainMenuMuteButtonText; // Text of mute button

        [Header("Pause Menu Elements")]
        [SerializeField] public GameObject pauseMenuPanel; // Pause Menu Panel
        [SerializeField] private Button pauseMenuResumeButton; // Button to resume game
        [SerializeField] private Button pauseMenuMainMenuButton; // Button to go to main menu

        [Header("Game Over Menu Elements")]
        [SerializeField] public GameObject gameOverMenuPanel; // Game Over Menu Panel
        [SerializeField] private Button gameOverMenuRestartButton; // Button to restart game
        [SerializeField] private Button gameOverMenuMainMenuButton; // Another button to go to main menu


        // Private Services
        private GameService gameService;

        public void Init(GameService _gameService)
        {
            gameService = _gameService;
        }

        private void Start()
        {
            mainMenuPlayButton.onClick.AddListener(gameService.GetGameController().PlayGame); // Add listener to play button
            mainMenuQuitButton.onClick.AddListener(gameService.GetGameController().QuitGame); // Add listener to quit button
            mainMenuMuteButton.onClick.AddListener(gameService.GetGameController().MuteGame); // Add listener to mute button

            pauseMenuResumeButton.onClick.AddListener(gameService.GetGameController().ResumeGame); // Add listener to resume button
            pauseMenuMainMenuButton.onClick.AddListener(gameService.GetGameController().MainMenu); // Add listener to main menu button

            gameOverMenuRestartButton.onClick.AddListener(gameService.GetGameController().RestartGame); // Add listener to restart button
            gameOverMenuMainMenuButton.onClick.AddListener(gameService.GetGameController().MainMenu); // Add listener to another main menu button
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