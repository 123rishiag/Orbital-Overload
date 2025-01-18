using ServiceLocator.Actor;
using ServiceLocator.Event;
using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public abstract class PowerUpController
    {
        // Private Variables
        protected PowerUpModel powerUpModel;
        protected PowerUpView powerUpView;

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
            eventService.OnGetGameControllerEvent.Invoke<GameController>().
                StartManagedCoroutine(PowerUp(_actorController)); // Activate power-up effect
        }

        private IEnumerator PowerUp(ActorController _actorController)
        {
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.PowerUpPickup);
            eventService.OnGetUIControllerEvent.Invoke<UIController>().
                GetUIView().UpdatePowerUpText(powerUpModel.PowerUpType, powerUpModel.PowerUpDuration); // Show power-up text
            EnablePowerUp(_actorController);
            yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
            DisablePowerUp(_actorController);
            eventService.OnGetUIControllerEvent.Invoke<UIController>().
                GetUIView().HidePowerUpText(); // Hide power-up text
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