using EcsCore;
using Leopotam.Ecs;
using UnityEngine;

namespace TheTalesOfTwo.Core.Obstacles
{
    [EcsSystem(typeof(CoreModule))]
    public class ObstacleRemoveSystem : IEcsRunLateSystem
    {
        private EcsFilter<ObstacleComponent> _filter;
        private ObstaclesPool _pool;
        public void RunLate()
        {
            foreach (var i in _filter)
            {
                ref var obstacle = ref _filter.Get1(i);
                obstacle.lifetime -= Time.deltaTime;
                if (obstacle.lifetime <= 0)
                {
                    _pool.Return(obstacle.obstacleView);
                    _filter.GetEntity(i).Destroy();
                }
            }
        }
    }
}