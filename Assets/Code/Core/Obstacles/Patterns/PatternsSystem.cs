using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Obstacles.Patterns
{
    [EcsSystem(typeof(ObstaclesModule))]
    public class PatternsSystem : IEcsRunSystem, IEcsRunLateSystem
    {
        private EcsFilter<PatternComponent, TimeComponent> _patterns;
        private EcsFilter<PauseEvent> _pauseFilter;
        public void Run()
        {
            foreach (var i in _patterns)
            {
                ref var pattern = ref _patterns.Get1(i);
                ref var time = ref _patterns.Get2(i);
                pattern.delay -= time.deltaTime * time.factor;
                if (pattern.delay > 0)
                    continue;

                pattern.isReady = true;
                _patterns.GetEntity(i).Replace(new EcsOneFrame());
            }
        }

        public void RunLate()
        {
            if (_pauseFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _patterns)
            {
                ref var time = ref _patterns.Get2(i);
                time.factor = 0;
            }
        }
    }
}