using UnityEngine;
using UnityEngine.UI;

namespace TheTalesOfTwo.Core.Hp.GUI
{
    [RequireComponent(typeof(Image))]
    public class OneHpView : MonoBehaviour
    {
        [SerializeField]
        private Sprite _fullHeart;

        [SerializeField]
        private Sprite _emptyHeart;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetEmpty(bool isEmpty)
        {
            _image.sprite = isEmpty ? _emptyHeart : _fullHeart;
        }
    }
}