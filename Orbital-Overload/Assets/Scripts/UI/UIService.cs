using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIService
    {
        // Private Variables
        private UIController uiController;

        public UIService(UIView _uiCanvas, GameService _gameService)
        {
            // Setting Variables
            uiController = new UIController(_uiCanvas, _gameService);
        }

        // Getters
        public UIController GetUIController()
        {
            return uiController;
        }
    }
}