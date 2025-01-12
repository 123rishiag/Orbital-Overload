using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class UIView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text healthText; // Text for displaying health
        [SerializeField] private TMP_Text scoreText; // Text for displaying score
        [SerializeField] private GameObject powerUpBar; // Power Up Bar
        [SerializeField] private TMP_Text powerUpText; // Text for displaying power-up status

        [Header("Pause Menu Elements")]
        [SerializeField] public GameObject pauseMenuPanel; // Pause Menu Panel
        [SerializeField] public Button pauseMenuResumeButton; // Button to resume game
        [SerializeField] public Button pauseMenuMainMenuButton; // Button to go to main menu

        [Header("Game Over Menu Elements")]
        [SerializeField] public GameObject gameOverMenuPanel; // Game Over Menu Panel
        [SerializeField] public Button gameOverMenuRestartButton; // Button to restart game
        [SerializeField] public Button gameOverMenuMainMenuButton; // Another button to go to main menu

        [Header("Main Menu Elements")]
        [SerializeField] public GameObject mainMenuPanel; // Main Menu Panel
        [SerializeField] public Button mainMenuPlayButton; // Button to start the game
        [SerializeField] public Button mainMenuQuitButton; // Button to quit the game
        [SerializeField] public Button mainMenuMuteButton; // Button to mute/unmute the game
        [SerializeField] public TMP_Text mainMenuMuteButtonText; // Text of mute button

        // Setters
        public void UpdateHealthText(int _health)
        {
            healthText.text = "Health: " + _health; // Display health
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
    }
}