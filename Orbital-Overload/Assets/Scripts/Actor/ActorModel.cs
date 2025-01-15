using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorModel
    {
        public ActorModel(ActorData _actorData)
        {
            ActorType = _actorData.actorType;
            ActorColor = _actorData.actorColor;
            ActorShooterColor = _actorData.actorShooterColor;
            MaxHealth = _actorData.maxHealth;
            CurrentHealth = MaxHealth;
            MoveSpeed = _actorData.moveSpeed;
            ShootSpeed = _actorData.shootSpeed;
            ShootCooldown = _actorData.shootCooldown;
            CurrentScore = 0;
            IsShieldActive = false;
            IsHoming = false;
        }

        // Getters & Setters
        public ActorType ActorType { get; private set; } // Actor Type
        public Color ActorColor { get; private set; } // Actor Color
        public Color ActorShooterColor { get; private set; } // Actor's Shooter Color
        public int MaxHealth { get; private set; } // Maximum health
        public int CurrentHealth { get; set; } // Current health
        public float MoveSpeed { get; private set; } // Movement speed
        public float ShootSpeed { get; private set; } // Speed of shooting
        public float ShootCooldown { get; set; } // Cooldown between shots
        public int CurrentScore { get; set; } // Current player score
        public bool IsShieldActive { get; set; } // Whether the shield is active
        public bool IsHoming { get; set; } // Whether projectiles are homing
    }
}