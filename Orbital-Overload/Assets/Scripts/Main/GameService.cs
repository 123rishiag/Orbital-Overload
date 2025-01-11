using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Private Variables
        [Header("Core Components")]
        [SerializeField] private UIController uiCanvas;

        // Private Services
        private UIService uiService;


        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            uiService = new UIService(uiCanvas);
        }
    }
}