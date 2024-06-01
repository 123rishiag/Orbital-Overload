using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Pause menu UI
    [SerializeField] private GameObject gameOverMenuUI; // Game over menu UI

    [SerializeField] private Button resumeButton; // Button to resume game
    [SerializeField] private Button restartButton; // Button to restart game
    [SerializeField] private Button mainMenuButton1; // Button to go to main menu
    [SerializeField] private Button mainMenuButton2; // Another button to go to main menu

    private bool isPaused = false; // Whether the game is paused
    private bool canPause = true; // Whether the game can be paused

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame); // Add listener to resume button
        restartButton.onClick.AddListener(RestartGame); // Add listener to restart button
        mainMenuButton1.onClick.AddListener(LobbyScreen); // Add listener to main menu button
        mainMenuButton2.onClick.AddListener(LobbyScreen); // Add listener to another main menu button
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
            pauseMenuUI.SetActive(true); // Show pause menu
            isPaused = true;
            SoundManager.Instance.PlayEffect(SoundType.GamePause); // Play pause sound effect
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseMenuUI.SetActive(false); // Hide pause menu
        isPaused = false;
        SoundManager.Instance.PlayEffect(SoundType.GamePause); // Play resume sound effect
    }

    public void GameOver()
    {
        canPause = false; // Disable pausing
        Time.timeScale = 0f; // Stop time
        gameOverMenuUI.SetActive(true); // Show game over menu
        SoundManager.Instance.PlayEffect(SoundType.GameOver); // Play game over sound effect
    }

    private void RestartGame()
    {
        canPause = true;
        Time.timeScale = 1f; // Restart time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
        SoundManager.Instance.PlayEffect(SoundType.GameStart); // Play game start sound effect
    }

    private void LobbyScreen()
    {
        SceneManager.LoadScene(0); // Load main menu scene
        SoundManager.Instance.PlayEffect(SoundType.ButtonQuit); // Play quit sound effect
    }
}