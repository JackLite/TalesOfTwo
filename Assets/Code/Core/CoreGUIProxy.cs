using TheTalesOfTwo.Core.GameOver.GUI;
using TheTalesOfTwo.Core.Hints;
using TheTalesOfTwo.Core.Hp.GUI;
using TheTalesOfTwo.Core.LevelProgress.GUI;
using UnityEngine;

namespace TheTalesOfTwo.Core
{
    public class CoreGUIProxy : MonoBehaviour
    {
        [field:SerializeField]
        public HpBarView HpBarView { get; private set; }
        
        [field:SerializeField]
        public GameOverScreenView GameOverScreen { get; private set; }
        
        [field:SerializeField]
        public LevelProgressView LevelProgressView { get; private set; }
        
        [field:SerializeField]
        public HintsView HintsView { get; private set; }
    }
}