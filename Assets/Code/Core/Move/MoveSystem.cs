using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Move
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveSystem : IEcsRunLateSystem
    {
        private EcsFilter<MoveComponent> _filter;
        private EcsFilter<AvatarHitEvent> _hitFilter;
        public void RunLate()
        {
            if (_hitFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _filter)
            {
                ref var move = ref _filter.Get1(i);
                if (move.canBePaused)
                    _filter.GetEntity(i).Replace(new PauseTag());
            }
        }
    }
}