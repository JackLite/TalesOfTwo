using System;
using System.Linq;
using EcsCore;
using Leopotam.Ecs;
using Newtonsoft.Json.Linq;
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
        public void PreInit()
        {
            var delay = 0f;
            for (var i = 0; i < PATTERNS_COUNT; ++i)
            {
                var patternRaw = _container.GetRandom();
                var obstacles = GetObstacles(patternRaw);
                CreatePattern(delay, obstacles);
                delay += _settings.DelayBetweenPatterns;
            }
        }

        private void CreatePattern(float delay, JArray obstacles)
        {
            var pattern = new PatternComponent
            {
                delay = delay,
                obstacles = obstacles.Select(t => t.ToString()).ToArray()
            };
            _world.NewEntity().Replace(pattern).Replace(new TimeComponent { factor = 1 });
        }

        private static JArray GetObstacles(string patternRaw)
        {
            var obstacles = JToken.Parse(patternRaw).Value<JArray>("obstacles");
            if (obstacles == null)
                throw new NullReferenceException("Can't find obstacles in " + patternRaw);
            return obstacles;
        }
    }
}