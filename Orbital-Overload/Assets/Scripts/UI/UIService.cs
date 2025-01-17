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

        public void Init(GameController _gameController)
        {
            // Setting Variables
            uiController = new UIController(uiCanvas, _gameController);
        }

        // Getters
        public UIController GetUIController()
        {
            return uiController;
        }
    }
}