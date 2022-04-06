using TheTalesOfTwo.Core.Collision;
using TheTalesOfTwo.Core.Move;
using UnityEngine;

namespace TheTalesOfTwo.Core.Avatars
{
    public class AvatarView : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [field:SerializeField]
        public MoveView MoveView { get; private set; }
        
        [field:SerializeField]
        public CollisionMono Collision { get; private set; }
        
        private static readonly int isRun = Animator.StringToHash("isRun");
        
        public void SetRun()
        {
            _animator.SetBool(isRun, true);
        }
    }
}