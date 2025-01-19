using ServiceLocator.Event;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIController
    {
        // Private Variables
        private UIView uiView;
        private GameController gameController;

        public UIController(UIView _uiCanvas, EventService _eventService)
        {
            // Setting Variables
            uiView = _uiCanvas.GetComponent<UIView>();
            uiView.gameObject.SetActive(true);
            gameController = _eventService.OnGetGameControllerEvent.Invoke<GameController>();

            // Adding Listeners
            uiView.pauseMenuResumeButton.onClick.AddListener(gameController.PlayGame);
            uiView.pauseMenuMainMenuButton.onClick.AddListener(gameController.MainMenu);

            uiView.gameOverMenuRestartButton.onClick.AddListener(gameController.RestartGame);
            uiView.gameOverMenuMainMenuButton.onClick.AddListener(gameController.MainMenu);

            uiView.mainMenuPlayButton.onClick.AddListener(gameController.PlayGame);
            uiView.mainMenuQuitButton.onClick.AddListener(gameController.QuitGame);
            uiView.mainMenuMuteButton.onClick.AddListener(gameController.MuteGame);
        }

        public void Destroy()
        {
            // Removing Listeners
            uiView.pauseMenuResumeButton.onClick.RemoveListener(gameController.PlayGame);
            uiView.pauseMenuMainMenuButton.onClick.RemoveListener(gameController.MainMenu);

            uiView.gameOverMenuRestartButton.onClick.RemoveListener(gameController.RestartGame);
            uiView.gameOverMenuMainMenuButton.onClick.RemoveListener(gameController.MainMenu);

            uiView.mainMenuPlayButton.onClick.RemoveListener(gameController.PlayGame);
            uiView.mainMenuQuitButton.onClick.RemoveListener(gameController.QuitGame);
            uiView.mainMenuMuteButton.onClick.RemoveListener(gameController.MuteGame);
        }

        public void Reset() => uiView.HidePowerUpText();

        // Getters
        public UIView GetUIView() => uiView;
    }
}