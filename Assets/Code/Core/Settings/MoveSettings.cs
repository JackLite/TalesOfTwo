using System;
using UnityEngine;

namespace TheTalesOfTwo.Core.Settings
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