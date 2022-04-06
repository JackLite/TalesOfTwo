using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Environment;
using TheTalesOfTwo.Core.Lines;
using TheTalesOfTwo.Core.Obstacles;
using UnityEngine;

namespace TheTalesOfTwo.Core
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

        [field:SerializeField]
        public EnvironmentContainer EnvironmentContainer { get; private set; }
    }
}