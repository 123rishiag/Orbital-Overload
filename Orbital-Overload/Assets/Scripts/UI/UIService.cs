using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIService
    {
        // Private Variables
        private UIController uiController;

        public UIService(UIController _uiCanvas, GameService _gameService)
        {
            // Setting Variables
            uiController = _uiCanvas.GetComponent<UIController>();

            uiController.Init(_gameService);
        }

        // Getters
        public UIController GetUIController()
        {
            return uiController;
        }
    }
}