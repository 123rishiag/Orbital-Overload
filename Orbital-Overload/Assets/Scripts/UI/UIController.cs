using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIController
    {
        // Private Variables
        private UIView uiView;

        public UIController(UIView _uiCanvas, GameService _gameService)
        {
            // Setting Variables
            uiView = _uiCanvas.GetComponent<UIView>();

            // Adding Listeners
            uiView.pauseMenuResumeButton.onClick.AddListener(_gameService.GetGameController().ResumeGame); // Add listener to resume button
            uiView.pauseMenuMainMenuButton.onClick.AddListener(_gameService.GetGameController().MainMenu); // Add listener to main menu button

            uiView.gameOverMenuRestartButton.onClick.AddListener(_gameService.GetGameController().RestartGame); // Add listener to restart button
            uiView.gameOverMenuMainMenuButton.onClick.AddListener(_gameService.GetGameController().MainMenu); // Add listener to another main menu button

            uiView.mainMenuPlayButton.onClick.AddListener(_gameService.GetGameController().PlayGame); // Add listener to play button
            uiView.mainMenuQuitButton.onClick.AddListener(_gameService.GetGameController().QuitGame); // Add listener to quit button
            uiView.mainMenuMuteButton.onClick.AddListener(_gameService.GetGameController().MuteGame); // Add listener to mute button
        }

        // Getters
        public UIView GetUIView() => uiView;
    }
}