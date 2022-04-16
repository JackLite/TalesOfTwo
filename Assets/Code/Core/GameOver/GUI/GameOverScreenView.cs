using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheTalesOfTwo.Core.GameOver.GUI
{
    public class GameOverScreenView : MonoBehaviour
    {
        [SerializeField]
        private Button _returnToMenu;

        public event Action OnReturnToMenu;

        private void Awake()
        {
            _returnToMenu.onClick.AddListener(() => OnReturnToMenu?.Invoke());
        }
    }
}