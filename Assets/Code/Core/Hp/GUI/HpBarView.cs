using System.Collections.Generic;
using UnityEngine;

namespace TheTalesOfTwo.Core.Hp.GUI
{
    public class HpBarView : MonoBehaviour
    {
        [SerializeField]
        private OneHpView _oneHpPrefab;

        private readonly Stack<OneHpView> _hpFullViewStack = new();
        private readonly Stack<OneHpView> _hpEmptyView = new();

        public void CreateOneHp()
        {
            var newHp = Instantiate(_oneHpPrefab, Vector3.zero, Quaternion.identity, transform);
            _hpFullViewStack.Push(newHp);
        }

        public void UpdateHp(int hp)
        {
            var diff = hp - _hpFullViewStack.Count;
            if (diff < 0)
                SubtractHp(-diff);
            else
                AddHp(diff);
        }

        private void SubtractHp(int count)
        {
            for (var _ = 0; _ < count; _++)
            {
                var hp = _hpFullViewStack.Pop();
                hp.SetEmpty(true);
                _hpEmptyView.Push(hp);
            }
        }

        private void AddHp(int count)
        {
            for (var _ = 0; _ < count; _++)
            {
                var hp = _hpEmptyView.Pop();
                hp.SetEmpty(false);
                _hpFullViewStack.Push(hp);
            }
        }

    }
}