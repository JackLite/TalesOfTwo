using UnityEngine;

namespace TheTalesOfTwo.Core.Hints
{
    [RequireComponent(typeof(RectTransform))]
    public class HintsView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform left;

        [SerializeField]
        private RectTransform right;

        private int _lines = 3;
        private RectTransform _parentRect;
        private void Awake()
        {
            _parentRect = GetComponent<RectTransform>();
        }

        public void Init(int lines)
        {
            _lines = lines;
        }
        
        public void ShowLeft(int line, bool isTop)
        {
            Show(left, line, isTop);
        }

        public void ShowRight(int line, bool isTop)
        {
            Show(right, line, isTop);
        }

        private void Show(RectTransform arrow, int line, bool isTop)
        {
            arrow.anchorMin = new Vector2(.5f, 0);
            arrow.anchorMax = new Vector2(.5f, 0);
            var onePieceSize = _parentRect.rect.height / _lines;
            var y = onePieceSize * (line - 1) + onePieceSize * .5f;
            var oldPos = arrow.anchoredPosition;
            arrow.anchoredPosition = new Vector2(oldPos.x, y);
            arrow.localRotation = Quaternion.Euler(0, 0, isTop ? 0 : 180);
            arrow.gameObject.SetActive(true);
        }

        public void HideLeft()
        {
            left.gameObject.SetActive(false);
        }

        public void HideRight()
        {
            right.gameObject.SetActive(false);
        }
    }
}