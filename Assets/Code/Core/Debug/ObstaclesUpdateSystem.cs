using System;
using EcsCore;
using FlyAdventure.Core.Move;
using FlyAdventure.Core.Move.Linear;
using FlyAdventure.Core.Obstacles;
using FlyAdventure.Core.Settings;
using Leopotam.Ecs;

namespace FlyAdventure.Core.Debug
{
    [EcsSystem(typeof(DebugModule))]
    public class ObstaclesUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<ObstacleComponent, MoveComponent> _filter;
        private CoreSettings _settings;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var move = ref _filter.Get2(i);
                move.speed = _settings.ObstaclesSpeed;
            }
        }
    }
}