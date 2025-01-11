using ServiceLocator.Sound;

namespace ServiceLocator.UI
{
    public class UIService
    {
        // Private Variables
        private UIController uiController;

        public UIService(UIController _uiCanvas, SoundService _soundService)
        {
            // Setting Variables
            uiController = _uiCanvas.GetComponent<UIController>();

            uiController.Init(_soundService);
        }

        // Getters
        private UIController GetUIController()
        {
            return uiController;
        }
    }
}