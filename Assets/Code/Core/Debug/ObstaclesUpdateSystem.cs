using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Obstacles;
using TheTalesOfTwo.Core.Settings;

namespace TheTalesOfTwo.Core.Debug
{
    [EcsSystem(typeof(DebugModule))]
    public class ObstaclesUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<ObstacleComponent, MoveComponent> _filter;
        private CoreSettings _settings;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var move = ref _filter.Get2(i);
                move.speed = _settings.ObstaclesSpeed;
            }
        }
    }
}