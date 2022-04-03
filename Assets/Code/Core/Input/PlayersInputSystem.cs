using EcsCore;
using FlyAdventure.Core.Move.ToLine;
using Leopotam.Ecs;
using UnityEngine;

namespace FlyAdventure.Core.Input
{
    [EcsSystem(typeof(CoreModule))]
    public class PlayersInputSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter<InputComponent, MoveToLine> _filter;
        private readonly InputAssetClass _playerInput;

        public PlayersInputSystem()
        {
            _playerInput = new InputAssetClass();
        }

        public void PreInit()
        {
            _playerInput.Enable();
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var l = ref _filter.Get2(i);
                if (l.toLine != l.fromLine)
                    continue;
                var action = input.isRight ? "MoveRight" : "MoveLeft";
                var val = _playerInput.FindAction(action).ReadValue<float>();
                if (val == 0)
                    continue;
                l.toLine += val > 0 ? 1 : -1;
                l.toLine = Mathf.Clamp(l.toLine, 1, 3);
                l.remain = 1;
            }
        }
    }
}