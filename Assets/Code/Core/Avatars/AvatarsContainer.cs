using FlyAdventure.Core.Move;
using UnityEngine;

namespace FlyAdventure.Core.Avatars
{
    public class AvatarsContainer : MonoBehaviour
    {
        [field:SerializeField]
        public AvatarView LeftAvatar { get; private set; }

        [field:SerializeField]
        public AvatarView RightAvatar { get; private set; }
    }
}