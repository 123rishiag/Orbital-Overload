using ServiceLocator.Actor;
using ServiceLocator.PowerUp;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Vision;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Inspector Variables
        [Header("Sound Components")]
        [SerializeField] public SoundConfig soundConfig;
        [SerializeField] public AudioSource sfxSource;
        [SerializeField] public AudioSource bgSource;

        [Header("UI Components")]
        [SerializeField] public UIView uiCanvas;

        [Header("Camera Components")]
        [SerializeField] public Camera mainCamera; // Main camera reference

        [Header("Game Configs")]
        [SerializeField] public CameraConfig cameraConfig;
        [SerializeField] public ProjectileConfig projectileConfig;
        [SerializeField] public PowerUpConfig powerUpConfig;
        [SerializeField] public ActorConfig actorConfig;

        [Header("Object Pool Parent Panels")]
        [SerializeField] public Transform projectileParentPanel;
        [SerializeField] public Transform powerUpParentPanel;
        [SerializeField] public Transform actorParentPanel;

        // Private Variables

        private GameController gameController;

        private void Start()
        {
            gameController = new GameController(this);
        }

        private void Update()
        {
            gameController.Update();
        }
        private void FixedUpdate()
        {
            gameController.FixedUpdate();
        }
        private void LateUpdate()
        {
            gameController.LateUpdate();
        }

        public void OnDestroy()
        {
            gameController.Destroy();
        }
    }
}