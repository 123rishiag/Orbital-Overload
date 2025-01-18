using ServiceLocator.PowerUp;
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

        [Header("Animator")]
        [SerializeField] private Animator uiAnimator; // UI's Animator

        // Private Variables
        private static readonly int HEALTH_BAR_HASH = Animator.StringToHash("HealthBar");
        private static readonly int SCORE_BAR_HASH = Animator.StringToHash("ScoreBar");
        private static readonly int POWERUP_BAR_HASH = Animator.StringToHash("PowerUpBar");

        private void HealthBarAnimation()
        {
            uiAnimator.Play(HEALTH_BAR_HASH, 0, 0f);
        }
        private void ScoreBarAnimation()
        {
            uiAnimator.Play(SCORE_BAR_HASH, 0, 0f);
        }
        private void PowerUpBarAnimation()
        {
            uiAnimator.Play(POWERUP_BAR_HASH, 0, 0f);
        }

        // Setters
        public void UpdateHealthText(int _health)
        {
            healthText.text = "Health: " + _health; // Display health
            HealthBarAnimation();
        }
        public void UpdateScoreText(int _score)
        {
            scoreText.text = "Score: " + _score;
            ScoreBarAnimation();
        }
        public void UpdatePowerUpText(PowerUpType _powerUpType, float _powerUpDuration)
        {
            if (_powerUpType == PowerUpType.HealthPick || _powerUpType == PowerUpType.Teleport)
            {
                powerUpText.text = _powerUpType.ToString() + "ed.";
            }
            else
            {
                powerUpText.text = _powerUpType.ToString() + " activated for " + _powerUpDuration.ToString() + " seconds.";
            }
            powerUpBar.SetActive(true);
            PowerUpBarAnimation();
        }
        public void HidePowerUpText()
        {
            powerUpBar.SetActive(false);
        }
        public void SetMuteButtonText(bool _isMute)
        {
            mainMenuMuteButtonText.text = _isMute ? "Mute: Off" : "Mute: On"; // Toggle mute text
        }
    }
}