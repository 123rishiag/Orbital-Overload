using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "ScriptableObjects/SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        public SoundData[] soundList;
    }

    [Serializable]
    public struct SoundData
    {
        public SoundType soundType;
        public AudioClip soundClip;
    }
}