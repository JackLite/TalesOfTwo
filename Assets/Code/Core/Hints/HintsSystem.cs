using System;
using EcsCore;
using Leopotam.Ecs;
using Newtonsoft.Json.Linq;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Obstacles;
using TheTalesOfTwo.Core.Obstacles.Patterns;
using TheTalesOfTwo.Core.Settings;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Hints
{
    [EcsSystem(typeof(ObstaclesModule))]
    public class HintsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private HintsView _hintsView;
        private CoreSettings _coreSettings;
        private EcsFilter<PatternComponent> _patterns;
        private EcsFilter<HintComponent, TimeComponent> _hints;
        private EcsWorld _world;
        public void Init()
        {
            //TODO: брать число линий из настроек уровня
            _hintsView.Init(3);
        }

        public void Run()
        {
            ReadPatterns();
            ProcessHints();
        }
        private void ProcessHints()
        {
            foreach (var i in _hints)
            {
                ref var hint = ref _hints.Get1(i);
                ref var time = ref _hints.Get2(i);
                hint.delay -= time.factor * time.deltaTime;
                if (hint.delay > 0)
                    continue;

                if (hint.isRight)
                    _hintsView.ShowRight(hint.line, hint.isTop);
                else
                    _hintsView.ShowLeft(hint.line, hint.isTop);

                hint.duration -= time.factor * time.deltaTime;

                if (hint.duration <= 0)
                {
                    if (hint.isRight)
                        _hintsView.HideRight();
                    else
                        _hintsView.HideLeft();
                    _hints.GetEntity(i).Destroy();
                }
            }
        }

        private void ReadPatterns()
        {
            foreach (var i in _patterns)
            {
                ref var pattern = ref _patterns.Get1(i);
                if (!pattern.isReady)
                    continue;

                foreach (var group in pattern.obstacleGroups)
                {
                    var jToken = JToken.Parse(group);
                    var delay = jToken.Value<float>("delay");
                    var hints = jToken.Value<JArray>("hints");
                    if (hints == null)
                        continue;
                    foreach (var t in hints)
                    {
                        _world.NewEntity()
                              .Replace(new HintComponent
                              {
                                  line = t.Value<int>("line"),
                                  delay = delay + 2,
                                  isRight = t.Value<string>("side") == "right",
                                  duration = 1f,
                                  isTop = t.Value<string>("direction") == "top"
                              })
                              .Replace(new TimeComponent { factor = 1 })
                              .Replace(new CleanUpTag());
                    }
                }
            }
        }
    }
}