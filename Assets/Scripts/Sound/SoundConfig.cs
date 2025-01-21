using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "ScriptableObjects/SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        public SoundView soundPrefab;
        public float soundVolume;
        public float bgVolume;
        public SoundData[] soundData;
    }

    [Serializable]
    public struct SoundData
    {
        public SoundType soundType;
        public AudioClip soundClip;
    }
}