using UnityEngine;

namespace TheTalesOfTwo.Core.Avatars
{
    public class AvatarsContainer : MonoBehaviour
    {
        [field:SerializeField]
        public AvatarView LeftAvatar { get; private set; }

        [field:SerializeField]
        public AvatarView RightAvatar { get; private set; }
    }
}