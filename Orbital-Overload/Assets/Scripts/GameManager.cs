using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;

    public Button resumeButton;
    public Button restartButton;
    public Button mainMenuButton1;
    public Button mainMenuButton2;

    private bool isPaused = false;
    private bool canPause = true;

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton1.onClick.AddListener(LobbyScreen);
        mainMenuButton2.onClick.AddListener(LobbyScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (canPause == true)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            isPaused = true;
            SoundManager.Instance.PlayEffect(SoundType.GamePause);
        }
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        SoundManager.Instance.PlayEffect(SoundType.GamePause);
    }
    public void GameOver()
    {
        canPause = false;
        Time.timeScale = 0f;
        gameOverMenuUI.SetActive(true);
        SoundManager.Instance.PlayEffect(SoundType.GameOver);
    }
    private void RestartGame()
    {
        canPause = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.Instance.PlayEffect(SoundType.GameStart);
    }
    private void LobbyScreen()
    {
        SceneManager.LoadScene(0);
        SoundManager.Instance.PlayEffect(SoundType.ButtonQuit);
    }
}
