
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject lobbyMenuUI; // Lobby menu UI

    [SerializeField] private Button playButton; // Button to start the game
    [SerializeField] private Button quitButton; // Button to quit the game
    [SerializeField] private Button muteButton; // Button to mute/unmute the game

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame); // Add listener to play button
        quitButton.onClick.AddListener(QuitGame); // Add listener to quit button
        muteButton.onClick.AddListener(MuteGame); // Add listener to mute button
    }

    public void PlayGame()
    {
        Time.timeScale = 1f; // Ensure game time is running
        SceneManager.LoadScene(1); // Load the game scene
        SoundManager.Instance.PlayEffect(SoundType.GameStart); // Play game start sound effect
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); // Quit the application
    }

    private void MuteGame()
    {
        TextMeshProUGUI muteButtonText = muteButton.GetComponentInChildren<TextMeshProUGUI>();
        bool isMute = muteButtonText.text == "Mute: On";
        muteButtonText.text = isMute ? "Mute: Off" : "Mute: On"; // Toggle mute text
        SoundManager.Instance.MuteGame(); // Mute/unmute the game
        SoundManager.Instance.PlayEffect(SoundType.ButtonClick); // Play button click sound effect
    }
}