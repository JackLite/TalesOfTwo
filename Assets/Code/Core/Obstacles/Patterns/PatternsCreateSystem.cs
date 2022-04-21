using System;
using System.Linq;
using EcsCore;
using Leopotam.Ecs;
using Newtonsoft.Json.Linq;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Settings;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Obstacles.Patterns
{
    [EcsSystem(typeof(CoreModule))]
    public class PatternsCreateSystem : IEcsPreInitSystem
    {
        private const int PATTERNS_COUNT = 20; // TODO: move to level settings
        private PatternsRawContainer _container;
        private EcsWorld _world;
        private CoreSettings _settings;
        private EcsOneData<ObstaclesOneData> _obstaclesData;
        public void PreInit()
        {
            var delay = 0f;
            ref var obstaclesOneData = ref _obstaclesData.GetData();
            for (var i = 0; i < PATTERNS_COUNT; ++i)
            {
                var patternRaw = _container.GetRandom();
                var obstacleGroups = GetObstacleGroups(patternRaw);
                obstaclesOneData.totalCount += obstacleGroups.Count;
                CreatePattern(delay, obstacleGroups);
                delay += _settings.DelayBetweenPatterns;
            }
            obstaclesOneData.remainCount = obstaclesOneData.totalCount;
        }

        private void CreatePattern(float delay, JArray obstacleGroups)
        {
            var pattern = new PatternComponent
            {
                delay = delay,
                obstacleGroups = obstacleGroups.Select(t => t.ToString()).ToArray()
            };
            _world.NewEntity().Replace(pattern).Replace(new TimeComponent { factor = 1 }).Replace(new CleanUpTag());
        }

        private static JArray GetObstacleGroups(string patternRaw)
        {
            var obstacles = JToken.Parse(patternRaw).Value<JArray>("groups");
            if (obstacles == null)
                throw new NullReferenceException("Can't find obstacles in " + patternRaw);
            return obstacles;
        }
    }
}