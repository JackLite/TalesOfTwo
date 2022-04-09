using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Obstacles
{
    [EcsSystem(typeof(ObstaclesModule))]
    public class ObstacleRemoveSystem : IEcsRunLateSystem
    {
        private EcsFilter<ObstacleComponent, TimeComponent> _filter;
        private EcsFilter<AvatarHitEvent> _hitFilter;
        private ObstaclesPool _pool;
        public void RunLate()
        {
            if (_hitFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _filter)
            {
                ref var time = ref _filter.Get2(i);
                time.factor = 0;
            }

            foreach (var i in _filter)
            {
                ref var obstacle = ref _filter.Get1(i);
                ref var time = ref _filter.Get2(i);
                obstacle.lifetime -= time.deltaTime * time.factor;
                if (obstacle.lifetime <= 0)
                {
                    _pool.Return(obstacle.obstacleView);
                    _filter.GetEntity(i).Destroy();
                }
            }
        }
    }
}