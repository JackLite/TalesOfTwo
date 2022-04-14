using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Lines;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Move.Linear;
using TheTalesOfTwo.Core.Obstacles.Queue;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Settings;
using TheTalesOfTwo.Core.Time;
using UnityEngine;

namespace TheTalesOfTwo.Core.Obstacles
{
    [EcsSystem(typeof(ObstaclesModule))]
    public class ObstacleSpawnSystem : IEcsRunSystem, IEcsRunLateSystem
    {
        private EcsWorld _world;
        private ObstaclesPool _pool;
        private CoreSettings _settings;
        private EcsFilter<ObstacleQueueComponent, TimeComponent> _filter;
        private EcsFilter<PauseEvent> _pauseFilter;

        private float _lastSpawnTime;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var queue = ref _filter.Get1(i);
                ref var time = ref _filter.Get2(i);
                queue.delay -= time.deltaTime * time.factor;
                if (queue.delay > 0)
                    continue;

                var view = _pool.Get();
                view.transform.position = Vector3.zero;
                view.MoveView.SetY(CalculatePositionService.CalculateY(10, queue.line, 0.9f));
                var moveComponent = new MoveComponent
                {
                    speed = _settings.ObstaclesSpeed,
                    view = view.MoveView,
                    canBePaused = true,
                    factor = 1
                };
                _world.NewEntity()
                      .Replace(moveComponent)
                      .Replace(new ObstacleComponent { obstacleView = view, lifetime = 20 })
                      .Replace(new MoveLinear { direction = queue.isRight ? Vector2.right : Vector2.left });
                _filter.GetEntity(i).Destroy();
            }
        }
        public void RunLate()
        {
            if (_pauseFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _filter)
            {
                ref var time = ref _filter.Get2(i);
                time.factor = 0;
            }
        }
    }
}