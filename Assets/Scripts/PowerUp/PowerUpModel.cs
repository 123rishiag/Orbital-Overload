using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpModel
    {
        public PowerUpModel(PowerUpData _powerUpData)
        {
            Reset(_powerUpData);
        }

        public void Reset(PowerUpData _powerUpData)
        {
            PowerUpType = _powerUpData.powerUpType;
            PowerUpSprite = _powerUpData.powerUpSprite;
            PowerUpDuration = _powerUpData.powerUpDuration;
            PowerUpValue = _powerUpData.powerUpValue;
            PowerUpLifetime = _powerUpData.powerUpLifetime;
        }

        // Getters
        public PowerUpType PowerUpType { get; private set; } // Type of power-up
        public Sprite PowerUpSprite { get; private set; } // Sprite of power-up
        public float PowerUpDuration { get; private set; } // Duration of the power-up effect
        public float PowerUpValue { get; private set; } // Value of the power-up effect
        public float PowerUpLifetime { get; private set; } // Lifetime of the power-up before it expires
    }
}