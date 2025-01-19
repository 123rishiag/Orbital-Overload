using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.VFX;
using ServiceLocator.Vision;
using System;
using UnityEngine;

namespace ServiceLocator.Event
{
    public class EventService
    {
        // Function to return GameController - Game Controller
        public EventController<Func<GameController>> OnGetGameControllerEvent { get; private set; }

        // Method to Play a particular Sound Effect - Sound Service
        public EventController<Action<SoundType>> OnPlaySoundEffectEvent { get; private set; }

        // Function to return UIController - UI Service
        public EventController<Func<UIController>> OnGetUIControllerEvent { get; private set; }

        // Method to Play a Shake Screen - Camera Service
        public EventController<Action<CameraShakeType>> OnDoShakeScreenEvent { get; private set; }

        // Method to Create a VFX - VFX Service
        public EventController<Action<VFXType, Vector3>> OnCreateVFXEvent { get; private set; }

        public EventService()
        {
            OnGetGameControllerEvent = new EventController<Func<GameController>>();
            OnPlaySoundEffectEvent = new EventController<Action<SoundType>>();
            OnGetUIControllerEvent = new EventController<Func<UIController>>();
            OnDoShakeScreenEvent = new EventController<Action<CameraShakeType>>();
            OnCreateVFXEvent = new EventController<Action<VFXType, Vector3>>();
        }
    }
}