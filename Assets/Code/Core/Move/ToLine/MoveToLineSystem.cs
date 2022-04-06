using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Lines;
using UnityEngine;

namespace TheTalesOfTwo.Core.Move.ToLine
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveToLineSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, MoveToLine> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var ml = ref _filter.Get2(i);
                if (ml.toLine == ml.fromLine)
                    continue;
                ref var m = ref _filter.Get1(i);

                var targetLinePos = CalculatePositionService.CalculateYForAvatar(10, ml.toLine);
                var currentLinePos = CalculatePositionService.CalculateYForAvatar(10, ml.fromLine);
                var currentPos = m.view.transform.position.y;
                var linePosDiff = targetLinePos - currentLinePos;
                var remainDistanceAbs = Mathf.Abs(currentPos - targetLinePos);

                var delta = CalcDelta(ml, m, linePosDiff);
                if (remainDistanceAbs >= Mathf.Abs(delta))
                {
                    m.view.AddDeltaY(delta);
                    ml.remain = remainDistanceAbs / Mathf.Abs(linePosDiff);
                }
                else
                {
                    m.view.SetY(targetLinePos);
                    ml.fromLine = ml.toLine;
                    ml.remain = 0;
                }
            }
        }
        private static float CalcDelta(in MoveToLine ml, in MoveComponent m, float linePosDiff)
        {
            var proceed = 1 - ml.remain;
            var factor = ml.ease.Evaluate(proceed) - proceed;
            var speed = m.speed + m.speed * factor;
            var delta = linePosDiff * speed * Time.deltaTime;
            return delta;
        }
    }
}