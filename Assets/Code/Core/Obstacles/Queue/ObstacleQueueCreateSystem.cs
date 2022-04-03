using EcsCore;
using FlyAdventure.Core.Obstacles.Patterns;
using Leopotam.Ecs;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace FlyAdventure.Core.Obstacles.Queue
{
    [EcsSystem(typeof(CoreModule))]
    public class ObstacleQueueCreateSystem : IEcsRunSystem
    {
        private EcsFilter<PatternComponent> _patternsFilter;
        private EcsWorld _world;
        public void Run()
        {
            foreach (var i in _patternsFilter)
            {
                ref var pattern = ref _patternsFilter.Get1(i);
                pattern.delay -= Time.deltaTime;
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
                    _world.NewEntity().Replace(obstacle);
                }
                _patternsFilter.GetEntity(i).Destroy();
            }
        }
    }
}