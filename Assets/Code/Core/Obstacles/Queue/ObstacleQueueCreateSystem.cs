using EcsCore;
using Leopotam.Ecs;
using Newtonsoft.Json.Linq;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Obstacles.Patterns;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Obstacles.Queue
{
    [EcsSystem(typeof(ObstaclesModule))]
    public class ObstacleQueueCreateSystem : IEcsRunSystem
    {
        private EcsFilter<PatternComponent> _patternsFilter;
        private EcsFilter<PauseEvent> _pauseFilter;
        private EcsWorld _world;
        public void Run()
        {
            foreach (var i in _patternsFilter)
            {
                ref var pattern = ref _patternsFilter.Get1(i);
                if (!pattern.isReady)
                    continue;

                foreach (var rawObstacle in pattern.obstacleGroups)
                {
                    var jToken = JToken.Parse(rawObstacle);
                    var delay = jToken.Value<float>("delay");
                    var obstacles = jToken.Value<JArray>("obstacles");
                    if (obstacles == null)
                        continue;
                    foreach (var t in obstacles)
                    {
                        var obstacle = new ObstacleQueueComponent
                        {
                            line = t.Value<int>("line"),
                            delay = delay,
                            isRight = t.Value<string>("direction") == "right"
                        };
                        _world.NewEntity()
                              .Replace(obstacle)
                              .Replace(new TimeComponent { factor = 1 })
                              .Replace(new CleanUpTag());
                    }
                }
            }
        }
    }
}