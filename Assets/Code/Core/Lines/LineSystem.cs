using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Settings;
using UnityEngine;

namespace TheTalesOfTwo.Core.Lines
{
    [EcsSystem(typeof(CoreModule))]
    public class LineSystem : IEcsPreInitSystem, IEcsInitSystem
    {
        private EcsFilter<TransformComponent, LineTopBorder> _filter;
        private CoreSettings _coreSettings;
        private LinesPool _linesPool;
        private EcsWorld _world;
        public void PreInit()
        {
            var lines = 3;
            for (var i = 1; i < lines; ++i)
            {
                var line = new LineTopBorder { line = i };
                var t = new TransformComponent { transform = _linesPool.Get() };
                _world.NewEntity().Replace(t).Replace(line).Replace(new CleanUpTag());
            }
        }
        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var t = ref _filter.Get1(i);
                ref var border = ref _filter.Get2(i);
                var borderY = CalculatePositionService.CalculateY(10, border.line, 1);
                t.transform.position = new Vector3(0, borderY, 0);
            }
        }
    }
}