using TheTalesOfTwo.Core.GameOver.GUI;
using TheTalesOfTwo.Core.Hp.GUI;
using UnityEngine;

namespace TheTalesOfTwo.Core
{
    public class CoreGUIProxy : MonoBehaviour
    {
        [field:SerializeField]
        public HpBarView HpBarView { get; private set; }
        
        [field:SerializeField]
        public GameOverScreenView GameOverScreen { get; private set; }
    }
}