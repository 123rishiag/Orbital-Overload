using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{

    public GameObject lobbyMenuUI;

    public Button playButton;
    public Button quitButton;
    public Button muteButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        muteButton.onClick.AddListener(MuteGame);
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        SoundManager.Instance.PlayEffect(SoundType.GameStart);
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    private void MuteGame()
    {
        TextMeshProUGUI muteButtonText = muteButton.GetComponentInChildren<TextMeshProUGUI>();
        bool isMute = muteButtonText.text == "Mute: On";
        muteButtonText.text = isMute ? "Mute: Off" : "Mute: On";
        SoundManager.Instance.MuteGame();
        SoundManager.Instance.PlayEffect(SoundType.ButtonClick);
    }

}
