using EcsCore;
using Leopotam.Ecs;
using Newtonsoft.Json.Linq;
using TheTalesOfTwo.Core.Obstacles.Patterns;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Obstacles.Queue
{
    [EcsSystem(typeof(ObstaclesModule))]
    public class ObstacleQueueCreateSystem : IEcsRunSystem, IEcsRunLateSystem
    {
        private EcsFilter<PatternComponent, TimeComponent> _patternsFilter;
        private EcsFilter<PauseEvent> _pauseFilter;
        private EcsWorld _world;
        public void Run()
        {
            foreach (var i in _patternsFilter)
            {
                ref var pattern = ref _patternsFilter.Get1(i);
                ref var time = ref _patternsFilter.Get2(i);
                pattern.delay -= time.deltaTime * time.factor;
                if (pattern.delay > 0)
                    continue;

                foreach (var rawObstacle in pattern.obstacles)
                {
                    var jToken = JToken.Parse(rawObstacle);
                    var obstacle = new ObstacleQueueComponent
                    {
                        line = jToken.Value<int>("line"),
                        delay = jToken.Value<float>("delay"),
                        isRight = jToken.Value<string>("direction") == "right"
                    };
                    _world.NewEntity().Replace(obstacle).Replace(new TimeComponent { factor = 1 });
                }
                _patternsFilter.GetEntity(i).Destroy();
            }
        }

        public void RunLate()
        {
            if (_pauseFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _patternsFilter)
            {
                ref var time = ref _patternsFilter.Get2(i);
                time.factor = 0;
            }
        }
    }
}