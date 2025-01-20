using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D rigidBody; // Rigidbody2D component of the projectile
        [SerializeField] public SpriteRenderer projectileSprite; // Projectile Sprite
        [SerializeField] private Animator projectileAnimator; // Projectile Animator
        [HideInInspector]
        public ProjectileController projectileController;

        // Private Variables
        private static readonly int IDLE_HASH = Animator.StringToHash("Idle");

        public void Init(ProjectileController _projectileController)
        {
            // Setting Variables
            projectileController = _projectileController;
            rigidBody = GetComponent<Rigidbody2D>();
            Reset();
        }

        public void Reset()
        {
            projectileSprite.material.color = projectileController.GetProjectileModel().ProjectileColor;
        }

        public void SetPosition(Vector2 _position)
        {
            transform.position = _position;
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }

        public void IdleAnimation()
        {
            projectileAnimator.Play(IDLE_HASH, 0, 0f);
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Actor"))
            {
                // Avoid collision with the owner
                ActorView actorView = _collider.gameObject.GetComponent<ActorView>();
                if ((actorView.actorController.GetActorModel().ActorType == ActorType.Player)
                    && (projectileController.GetProjectileModel().ProjectileOwnerActor == ActorType.Player)) return;
                if ((actorView.actorController.GetActorModel().ActorType != ActorType.Player)
                    && (projectileController.GetProjectileModel().ProjectileOwnerActor != ActorType.Player)) return;

                HideView();
                projectileController.PlayVFX();
            }
            else if (_collider.CompareTag("Projectile"))
            {
                HideView();
                projectileController.PlayVFX();
            }
        }
    }
}