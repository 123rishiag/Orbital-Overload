using ServiceLocator.Actor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Spawn
{
    public class SpawnService
    {
        // Private Variables
        private List<SpawnController> spawnControllers;

        // Private Services
        private ActorService actorService;

        public SpawnService()
        {
            // Setting Variables
            spawnControllers = new List<SpawnController>();
        }

        public void Init(ActorService _actorService)
        {
            // Setting Services
            actorService = _actorService;
        }

        public void CreateSpawnController(float _spawnInterval, float _spawnRadius,
            float _awayFromPlayerDistance, Action<Vector2> _spawnAction)
        {
            SpawnController spawnController = new SpawnController(_spawnInterval, _spawnRadius,
                _awayFromPlayerDistance, _spawnAction);
            spawnControllers.Add(spawnController);
        }

        public void Update()
        {
            // Using player position from ActorService for all SpawnControllers
            Vector2 playerPosition = actorService.GetPlayerActorController().GetActorView().GetPosition();

            foreach (var controller in spawnControllers)
            {
                controller.Update(playerPosition);
            }
        }
    }
}
