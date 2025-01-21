using Cinemachine;
using ServiceLocator.Actor;
using ServiceLocator.PowerUp;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.VFX;
using ServiceLocator.Vision;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Inspector Variables
        [Header("UI Components")]
        [SerializeField] public UIView uiCanvas;

        [Header("Camera Components")]
        [SerializeField] public CinemachineVirtualCamera virtualCamera;

        [Header("Game Configs")]
        [SerializeField] public VFXConfig vfxConfig;
        [SerializeField] public SoundConfig soundConfig;
        [SerializeField] public CameraConfig cameraConfig;
        [SerializeField] public ProjectileConfig projectileConfig;
        [SerializeField] public PowerUpConfig powerUpConfig;
        [SerializeField] public ActorConfig actorConfig;

        [Header("Object Pool Parent Panels")]
        [SerializeField] public Transform vfxParentPanel;
        [SerializeField] public Transform projectileParentPanel;
        [SerializeField] public Transform powerUpParentPanel;
        [SerializeField] public Transform actorParentPanel;
        [SerializeField] public Transform soundParentPanel;

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

        public void OnDestroy()
        {
            gameController.Destroy();
        }
    }
}