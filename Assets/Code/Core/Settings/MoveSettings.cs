using System;
using UnityEngine;

namespace FlyAdventure.Core.Settings
{
    [Serializable]
    public class MoveSettings
    {
        [field:SerializeField]
        public float AvatarSpeed { get; private set; }

        [field:SerializeField]
        public AnimationCurve ease;
    }
}