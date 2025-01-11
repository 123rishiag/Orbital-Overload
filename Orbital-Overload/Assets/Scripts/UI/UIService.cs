namespace ServiceLocator.UI
{
    public class UIService
    {
        // Private Variables
        private UIController uiController;

        public UIService(UIController _uiCanvas)
        {
            // Setting Variables
            uiController = _uiCanvas.GetComponent<UIController>();
        }

        // Getters
        private UIController GetUIController()
        {
            return uiController;
        }
    }
}