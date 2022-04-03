using FlyAdventure.Core.Move;
using UnityEngine;

namespace FlyAdventure.Core.Obstacles
{
    public class ObstacleView : MonoBehaviour
    {
        [field:SerializeField]
        public MoveView MoveView { get; private set; }
    }
}