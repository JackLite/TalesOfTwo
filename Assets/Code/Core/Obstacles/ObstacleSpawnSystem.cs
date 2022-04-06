using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Lines;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Move.Linear;
using TheTalesOfTwo.Core.Obstacles.Queue;
using TheTalesOfTwo.Core.Settings;
using UnityEngine;

namespace TheTalesOfTwo.Core.Obstacles
{
    [EcsSystem(typeof(CoreModule))]
    public class ObstacleSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private ObstaclesPool _pool;
        private CoreSettings _settings;
        private EcsFilter<ObstacleQueueComponent> _filter;

        private float _lastSpawnTime;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var queue = ref _filter.Get1(i);
                queue.delay -= Time.deltaTime;
                if (queue.delay > 0)
                    continue;

                var view = _pool.Get();
                view.transform.position = Vector3.zero;
                view.MoveView.SetY(CalculatePositionService.CalculateY(10, queue.line, 0.9f));
                _world.NewEntity()
                      .Replace(new ObstacleComponent { obstacleView = view, lifetime = 20 })
                      .Replace(new MoveComponent { speed = _settings.ObstaclesSpeed, view = view.MoveView })
                      .Replace(new MoveLinear { direction = queue.isRight ? Vector2.right : Vector2.left });
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}