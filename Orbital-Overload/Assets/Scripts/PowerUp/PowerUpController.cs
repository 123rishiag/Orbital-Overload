using ServiceLocator.Actor;
using ServiceLocator.Event;
using ServiceLocator.Main;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public abstract class PowerUpController
    {
        // Private Variables
        protected PowerUpModel powerUpModel;
        protected PowerUpView powerUpView;
        private Dictionary<PowerUpType, Coroutine> activePowerUps;

        // Private Services
        protected EventService eventService;

        public PowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            EventService _eventService)
        {
            // Setting Variables
            powerUpModel = new PowerUpModel(_powerUpData);
            powerUpView = Object.Instantiate(_powerUpPrefab, _spawnPosition, Quaternion.identity, _powerUpParentPanel).
                GetComponent<PowerUpView>();
            powerUpView.Init(this);

            // Setting Services
            eventService = _eventService;

            // Setting Elements
            activePowerUps = new Dictionary<PowerUpType, Coroutine>();
        }

        public void Reset(PowerUpData _powerUpData, Vector2 _spawnPosition)
        {
            powerUpModel.Reset(_powerUpData);
            powerUpView.Reset();
            powerUpView.SetPosition(_spawnPosition);
            powerUpView.ShowView();
        }

        protected abstract void EnablePowerUp(ActorController _actorController);
        protected abstract void DisablePowerUp(ActorController _actorController);

        public void ActivatePowerUp(ActorController _actorController)
        {
            // Stopping the existing power-up coroutine if it's already active
            if (activePowerUps.ContainsKey(powerUpModel.PowerUpType))
            {
                DisablePowerUp(_actorController);
                eventService.OnGetGameControllerEvent.Invoke<GameController>().
                StopManagedCoroutine(activePowerUps[powerUpModel.PowerUpType]);
            }

            // Starting a new coroutine for the power-up and store it
            Coroutine newPowerUpCoroutine =
                eventService.OnGetGameControllerEvent.Invoke<GameController>().
                StartManagedCoroutine(PowerUp(_actorController));
            activePowerUps[powerUpModel.PowerUpType] = newPowerUpCoroutine;
        }

        private IEnumerator PowerUp(ActorController _actorController)
        {
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.PowerUpPickup);
            eventService.OnGetUIControllerEvent.Invoke<UIController>().
                GetUIView().UpdatePowerUpText(powerUpModel.PowerUpType, powerUpModel.PowerUpDuration); // Show power-up text
            EnablePowerUp(_actorController);
            yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
            DisablePowerUp(_actorController);
            activePowerUps.Remove(powerUpModel.PowerUpType);
            eventService.OnGetUIControllerEvent.Invoke<UIController>().
                GetUIView().HidePowerUpText(); // Hide power-up text
        }

        public void PlayVFX()
        {
            eventService.OnCreateVFXEvent.Invoke(VFXType.Splatter, powerUpView.transform,
                powerUpModel.PowerUpColor);
        }

        public bool IsActive()
        {
            if (!powerUpView.gameObject.activeInHierarchy) return false;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(powerUpView.transform.position);
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                return false;
            }
            return true;
        }

        // Getters
        public PowerUpModel GetPowerUpModel() => powerUpModel;
        public PowerUpView GetPowerUpView() => powerUpView;
    }
}