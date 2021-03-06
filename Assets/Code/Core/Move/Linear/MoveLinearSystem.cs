using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Move.Linear
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveLinearSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, MoveLinear>.Exclude<PauseTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var move = ref _filter.Get1(i);
                ref var linear = ref _filter.Get2(i);
                move.view.SetVelocity(move.speed * move.factor * linear.direction * UnityEngine.Time.deltaTime);
            }
        }
    }
}