using UnityEngine;

namespace FlyAdventure.StartScreen
{
    public class StartScreenView : MonoBehaviour
    {
        [field:SerializeField]
        public StartScreenLoadingView StartScreenLoadingView { get; private set; }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}