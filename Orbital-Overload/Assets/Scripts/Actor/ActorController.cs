using ServiceLocator.Control;
using ServiceLocator.Event;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.Vision;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public abstract class ActorController
    {
        // Private Variables
        protected ActorModel actorModel;
        protected ActorView actorView;

        protected float lastShootTime; // Time of last shot
        protected float moveX; // X-axis movement input
        protected float moveY; // Y-axis movement input
        protected bool isShooting; // Shooting state
        protected Vector2 mouseDirection; // Direction of the mouse

        // Private Services
        protected EventService eventService;
        protected InputService inputService;
        protected ProjectileService projectileService;
        protected ActorService actorService;

        public ActorController(ActorData _actorData, ActorView _actorPrefab,
            Transform _actorParentPanel, Vector2 _spawnPosition,
            EventService _eventService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService)
        {
            // Setting Variables
            actorModel = new ActorModel(_actorData);
            actorView = Object.Instantiate(_actorPrefab, _spawnPosition, Quaternion.identity, _actorParentPanel).
                GetComponent<ActorView>();
            actorView.Init(this);

            lastShootTime = 0f;
            moveX = 0f;
            moveY = 0f;
            mouseDirection = Vector2.zero;

            // Setting Services
            eventService = _eventService;
            inputService = _inputService;
            projectileService = _projectileService;
            actorService = _actorService;

            // Update UI
            UpdateUI();
        }

        public void Reset(ActorData _actorData, Vector2 _spawnPosition)
        {
            actorModel.Reset(_actorData);
            actorView.Reset();
            actorView.SetPosition(_spawnPosition);
            actorView.ShowView();
            UpdateUI();
        }

        public void Update()
        {
            MovementInput(); // Handle movement input
            ShootInput(); // Handle shoot input
            RotateInput(); // Handle rotate input
        }
        public void FixedUpdate()
        {
            Move(); // Handle movement
            Shoot(); // Handle shooting
            Rotate(); // Handle rotation
        }

        protected abstract void MovementInput();
        protected abstract void ShootInput();
        protected abstract void RotateInput();

        private void Move()
        {
            Vector2 moveVector = new Vector2(moveX, moveY) * actorModel.MoveSpeed * Time.fixedDeltaTime;
            actorView.transform.Translate(moveVector, Space.World); // Move player
            //actorView.IdleAnimation();
        }
        private void Shoot()
        {
            if (isShooting && Time.time >= lastShootTime + actorModel.ShootCooldown)
            {
                lastShootTime = Time.time; // Update last shoot time
                projectileService.Shoot(actorModel.ActorType, actorModel.ShootSpeed, actorView.shootPoint,
                    actorModel.ProjectileType);
                ShootAnimation();
            }
        }
        private void ShootAnimation()
        {
            actorView.ShootAnimation();
            eventService.OnDoShakeScreenEvent.Invoke(CameraShakeType.Weak);
        }

        private void Rotate()
        {
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            actorView.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Rotate player to face mouse
        }

        public void Teleport(float _minDistance)
        {
            float maxDistance = _minDistance + 3f;

            float angle = Random.Range(0f, Mathf.PI * 2);

            float distance = Random.Range(_minDistance, maxDistance);

            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
            Vector3 newPosition = actorView.transform.position + offset;

            newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f); // Clamp x position
            newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f); // Clamp y position

            actorView.transform.position = newPosition; // Teleport player
        }

        public virtual void AddScore(int _score) { }

        public virtual void DecreaseHealth()
        {
            if (!actorModel.IsShieldActive)
            {
                actorModel.CurrentHealth -= 1; // Decrease health
                if (actorModel.CurrentHealth < 0)
                {
                    actorModel.CurrentHealth = 0;
                }
                eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ActorHurt);
            }
        }

        public virtual void IncreaseHealth(int _increaseHealth)
        {
            actorModel.CurrentHealth += _increaseHealth; // Increase health
            if (actorModel.CurrentHealth > actorModel.MaxHealth)
            {
                actorModel.CurrentHealth = actorModel.MaxHealth; // Clamp health to max
            }
            else
            {
                eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ActorHeal); // Play heal sound effect
            }
        }
        protected void UpdateUI()
        {
            UpdateHealthUI();
            UpdateScoreUI();
        }

        public virtual void UpdateHealthUI() { }
        public virtual void UpdateScoreUI() { }

        public bool IsAlive() => (actorModel.CurrentHealth > 0);

        // Getters
        public ActorModel GetActorModel() => actorModel;
        public ActorView GetActorView() => actorView;
    }
}