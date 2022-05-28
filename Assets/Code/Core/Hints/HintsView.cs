using Unity.Mathematics;
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
        private int _showCounterLeft;
        private int _showCounterRight;

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
            _showCounterLeft++;
        }

        public void ShowRight(int line, bool isTop)
        {
            Show(right, line, isTop);
            _showCounterRight++;
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
            _showCounterLeft = math.max(_showCounterLeft - 1, 0);
            if (_showCounterLeft == 0)
                left.gameObject.SetActive(false);
        }

        public void HideRight()
        {
            _showCounterRight = math.max(_showCounterRight - 1, 0);
            if (_showCounterRight == 0)
                right.gameObject.SetActive(false);
        }
    }
}