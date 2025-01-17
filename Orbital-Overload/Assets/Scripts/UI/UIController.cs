using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIController
    {
        // Private Variables
        private UIView uiView;

        public UIController(UIView _uiCanvas, GameController _gameController)
        {
            // Setting Variables
            uiView = _uiCanvas.GetComponent<UIView>();
            uiView.gameObject.SetActive(true);

            // Adding Listeners
            uiView.pauseMenuResumeButton.onClick.AddListener(_gameController.PlayGame); // Add listener to resume button
            uiView.pauseMenuMainMenuButton.onClick.AddListener(_gameController.MainMenu); // Add listener to main menu button

            uiView.gameOverMenuRestartButton.onClick.AddListener(_gameController.RestartGame); // Add listener to restart button
            uiView.gameOverMenuMainMenuButton.onClick.AddListener(_gameController.MainMenu); // Add listener to another main menu button

            uiView.mainMenuPlayButton.onClick.AddListener(_gameController.PlayGame); // Add listener to play button
            uiView.mainMenuQuitButton.onClick.AddListener(_gameController.QuitGame); // Add listener to quit button
            uiView.mainMenuMuteButton.onClick.AddListener(_gameController.MuteGame); // Add listener to mute button
        }

        // Getters
        public UIView GetUIView() => uiView;
    }
}