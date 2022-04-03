using FlyAdventure.Core.Collision;
using FlyAdventure.Core.Move;
using UnityEngine;

namespace FlyAdventure.Core.Avatars
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