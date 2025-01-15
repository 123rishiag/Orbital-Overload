using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIService
    {
        // Private Variables
        private UIView uiCanvas;
        private UIController uiController;

        public UIService(UIView _uiCanvas)
        {
            // Setting Variables
            uiCanvas = _uiCanvas;
        }

        public void Init(GameService _gameService)
        {
            // Setting Variables
            uiController = new UIController(uiCanvas, _gameService);
        }

        // Getters
        public UIController GetUIController()
        {
            return uiController;
        }
    }
}