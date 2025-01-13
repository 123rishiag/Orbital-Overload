namespace ServiceLocator.Enemy
{
    public class EnemyModel
    {
        public EnemyModel(EnemyData _enemyData)
        {
            MaxHealth = _enemyData.maxHealth;
            CurrentHealth = MaxHealth;
            CasualMoveSpeed = _enemyData.casualMoveSpeed;
            MoveSpeed = _enemyData.moveSpeed;
            ShootSpeed = _enemyData.shootSpeed;
            ShootCooldown = _enemyData.shootCooldown;
            HitScore = _enemyData.killScore;
            CurrentScore = 0;
            IsShieldActive = false;
            IsHoming = false; // Whether bullets are homing
        }

        // Getters & Setters
        public int MaxHealth { get; private set; } // Maximum health
        public int CurrentHealth { get; set; } // Current health
        public float CasualMoveSpeed { get; private set; } // Default move speed when idle
        public float MoveSpeed { get; private set; } // Movement speed
        public float ShootSpeed { get; private set; } // Speed of shooting
        public float ShootCooldown { get; set; } // Cooldown between shots
        public int HitScore { get; private set; } // Value for score increment
        public int CurrentScore { get; set; } // Current player score
        public bool IsShieldActive { get; set; } // Whether the shield is active
        public bool IsHoming { get; set; } // Whether bullets are homing
    }
}