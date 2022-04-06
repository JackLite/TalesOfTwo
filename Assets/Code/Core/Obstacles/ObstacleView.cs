using TheTalesOfTwo.Core.Move;
using UnityEngine;

namespace TheTalesOfTwo.Core.Obstacles
{
    public class ObstacleView : MonoBehaviour
    {
        [field:SerializeField]
        public MoveView MoveView { get; private set; }
    }
}