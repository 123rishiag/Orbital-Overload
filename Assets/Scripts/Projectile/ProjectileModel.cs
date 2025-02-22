using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileModel
    {
        public ProjectileModel(ProjectileData _projectileData, ActorType _projectileOwnerActor,
            float _shootSpeed, Color _projectileColor)
        {
            Reset(_projectileData, _projectileOwnerActor, _shootSpeed, _projectileColor);
        }

        public void Reset(ProjectileData _projectileData, ActorType _projectileOwnerActor,
            float _shootSpeed, Color _projectileColor)
        {
            ProjectileType = _projectileData.projectileType;
            ProjectileColor = _projectileColor;
            ProjectileOwnerActor = _projectileOwnerActor;
            ShootSpeed = _shootSpeed;
            HitScore = _projectileData.hitScore;
        }

        // Getters & Setters
        public ProjectileType ProjectileType { get; private set; } // Whether the projectile is homing or not
        public Color ProjectileColor { get; private set; } // Projectile Color
        public ActorType ProjectileOwnerActor { get; private set; } // Tag of the projectile's owner
        public float ShootSpeed { get; private set; } // Speed of homing
        public int HitScore { get; private set; } // Value for score increment
    }
}