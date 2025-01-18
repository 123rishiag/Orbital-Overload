using ServiceLocator.Event;

namespace ServiceLocator.UI
{
    public class UIService
    {
        // Private Variables
        private UIView uiCanvas;
        private UIController uiController;

        // Private Services
        private EventService eventService;

        public UIService(UIView _uiCanvas)
        {
            // Setting Variables
            uiCanvas = _uiCanvas;
        }

        public void Init(EventService _eventService)
        {
            // Setting Variables
            uiController = new UIController(uiCanvas, _eventService);

            // Setting Services
            eventService = _eventService;

            // Adding Listeners
            eventService.OnGetUIControllerEvent.AddListener(GetUIController);
        }

        public void Destroy()
        {
            uiController.Destroy();

            // Removing Listeners
            eventService.OnGetUIControllerEvent.RemoveListener(GetUIController);
        }

        public void Reset() => uiController.Reset();

        // Getters
        public UIController GetUIController() => uiController;
    }
}