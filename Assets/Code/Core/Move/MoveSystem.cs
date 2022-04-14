using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.AvatarHit;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Move
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveSystem : IEcsRunLateSystem
    {
        private EcsFilter<MoveComponent> _filter;
        private EcsFilter<MoveComponent, PauseTag> _pausedFilter;
        private EcsFilter<PauseEvent> _pauseFilter;
        private EcsFilter<UnpauseEvent> _unpauseFilter;
        public void RunLate()
        {
            if (_pauseFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _filter)
                {
                    ref var move = ref _filter.Get1(i);
                    if (move.canBePaused)
                        _filter.GetEntity(i).Replace(new PauseTag());
                }
            }

            if (_unpauseFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _pausedFilter)
                {
                    ref var move = ref _pausedFilter.Get1(i);
                    if (move.canBePaused)
                        _pausedFilter.GetEntity(i).Del<PauseTag>();
                }
            }
        }
    }
}