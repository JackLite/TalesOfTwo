using UnityEngine;

namespace TheTalesOfTwo.Core.Environment
{
    public class EnvironmentContainer : MonoBehaviour
    {
        [field:SerializeField]
        public SpriteRenderer Left { get; private set; }
        
        [field:SerializeField]
        public SpriteRenderer Right { get; private set; }
    }
}