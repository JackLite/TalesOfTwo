﻿using EcsCore;
using FlyAdventure.Core.Settings;
using Leopotam.Ecs;
using UnityEngine;

namespace FlyAdventure.Core.Move.Linear
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveLinearSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, MoveLinear> _filter;
        private MoveSettings _moveSettings;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var move = ref _filter.Get1(i);
                ref var linear = ref _filter.Get2(i);
                move.view.SetVelocity(move.speed * linear.direction * Time.deltaTime);
            }
        }
    }
}