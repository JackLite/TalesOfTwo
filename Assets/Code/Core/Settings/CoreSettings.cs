using System;
using UnityEngine;

namespace FlyAdventure.Core.Settings
{
    [Serializable]
    public class CoreSettings
    {
        [field:SerializeField]
        public Vector2 MainCameraZero { get; private set; }

        [field:SerializeField]
        public float DelayBetweenPatterns { get; private set; } = 3;
    }
}