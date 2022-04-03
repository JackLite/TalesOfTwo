using FlyAdventure.Core.Avatars;
using FlyAdventure.Core.Lines;
using FlyAdventure.Core.Obstacles;
using UnityEngine;

namespace FlyAdventure.Core
{
    /// <summary>
    /// Хранилище ссылок на объекты на сцене, не считая UI
    /// </summary>
    public class CoreViewProxy : MonoBehaviour
    {
        [field:SerializeField]
        public AvatarsContainer AvatarsContainer { get; private set; }

        [field:SerializeField]
        public LinesPool LinesPool { get; private set; }
        
        [field:SerializeField]
        public ObstaclesPool ObstaclesPool { get; private set; }
    }
}