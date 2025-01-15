using System;
using UnityEngine;

namespace ServiceLocator.Spawn
{
    public class SpawnController
    {
        private float spawnInterval;
        private float spawnRadius;
        private float awayFromPlayerDistance;
        private Action<Vector2> spawnAction;
        private float spawnTimer;

        public SpawnController(float _spawnInterval, float _spawnRadius, 
            float _awayFromPlayerDistance, Action<Vector2> _spawnAction)
        {
            spawnInterval = _spawnInterval;
            spawnRadius = _spawnRadius;
            awayFromPlayerDistance = _awayFromPlayerDistance;
            spawnAction = _spawnAction;
            spawnTimer = _spawnInterval;
        }

        public void Update(Vector2 _position)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                spawnTimer = spawnInterval; // Resetting timer
                Vector2 spawnPosition = CalculateSpawnPosition(_position);
                spawnAction.Invoke(spawnPosition); // Executing the spawn logic
            }
        }

        public Vector2 CalculateSpawnPosition(Vector2 _position)
        {
            // Calculate spawn position relative to the player
            Vector2 randomDirection = new Vector2(
                UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1,
                UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1
            );
            Vector2 awayFromPlayerOffset = randomDirection * awayFromPlayerDistance;

            return _position + awayFromPlayerOffset +
                   UnityEngine.Random.insideUnitCircle * spawnRadius;
        }
    }
}
