using System;
using UnityEngine;
using UnityEngine.UI;

namespace FlyAdventure.MainMenu
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;

        public event Action OnStart;

        private void Awake()
        {
            startButton.onClick.AddListener(() => OnStart?.Invoke());
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}