using ServiceLocator.Projectile;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorModel
    {
        public ActorModel(ActorData _actorData)
        {
            Reset(_actorData);
        }

        public void Reset(ActorData _actorData)
        {
            ActorType = _actorData.actorType;
            ActorSprite = _actorData.actorSprite;
            ActorShooterSprite = _actorData.actorShooterSprite;
            MaxHealth = _actorData.maxHealth;
            CurrentHealth = MaxHealth;
            MoveSpeed = _actorData.moveSpeed;
            ProjectileType = ProjectileType.Normal_Bullet;
            ShootSpeed = _actorData.shootSpeed;
            ShootCooldown = _actorData.shootCooldown;
            IsShieldActive = false;
            CurrentScore = 0;
        }

        // Getters & Setters
        public ActorType ActorType { get; private set; } // Actor Type
        public Sprite ActorSprite { get; private set; } // Actor Sprite
        public Sprite ActorShooterSprite { get; private set; } // Actor's Shooter Sprite
        public int MaxHealth { get; private set; } // Maximum health
        public int CurrentHealth { get; set; } // Current health
        public ProjectileType ProjectileType { get; set; } // Projectile Type of Actor
        public float MoveSpeed { get; private set; } // Movement speed
        public float ShootSpeed { get; private set; } // Speed of shooting
        public float ShootCooldown { get; set; } // Cooldown between shots
        public bool IsShieldActive { get; set; } // Whether the shield is active
        public int CurrentScore { get; set; } // Current Actor score
    }
}