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
        private static readonly int hurt = Animator.StringToHash("hurt");
        
        public void SetRun()
        {
            _animator.SetBool(isRun, true);
        }

        /// <summary>
        /// Set run animation speed
        /// </summary>
        /// <param name="speed">From 0 to 1</param>
        public void SetRunSpeed(float speed)
        {
            _animator.speed = speed;
        }

        public void SetHurt()
        {
            _animator.SetTrigger(hurt);
            _animator.SetBool(isRun, false);
        }
    }
}