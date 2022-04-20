using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheTalesOfTwo.Core.LevelProgress.GUI
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _levelProgress;

        [SerializeField]
        private RectTransform _left;

        [SerializeField]
        private RectTransform _right;

        [SerializeField]
        private Image _leftFill;

        [SerializeField]
        private Image _rightFill;

        private float _currentProgress = -1;

        /// <summary>
        /// Change progress view
        /// </summary>
        /// <param name="progress">Range from 0 to 1</param>
        public void SetProgress(float progress)
        {
            if (Math.Abs(_currentProgress - progress) < 0.00001f)
                return;

            var totalWidth = _levelProgress.sizeDelta.x;
            var targetOffset = totalWidth * .5f * progress;
            _leftFill.fillAmount = _rightFill.fillAmount = progress * .5f;
            _left.anchoredPosition = Vector2.right * targetOffset;
            _right.anchoredPosition = Vector2.left * targetOffset;
        }
    }
}