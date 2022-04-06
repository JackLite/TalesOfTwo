using System;
using UnityEngine;

namespace TheTalesOfTwo.Core.Settings
{
    [Serializable]
    public class CoreSettings
    {
        [field:SerializeField]
        public float DelayBetweenPatterns { get; private set; } = 3;

        [field:SerializeField]
        public float EnvironmentSpeed { get; private set; } = 100;
        
        [field:SerializeField]
        public float ObstaclesSpeed { get; private set; } = 10;

        [field:SerializeField]
        public int Health { get; private set; } = 3;
    }
}