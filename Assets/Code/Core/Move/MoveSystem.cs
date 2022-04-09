using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.AvatarHit;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Move
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveSystem : IEcsRunLateSystem
    {
        private EcsFilter<MoveComponent> _filter;
        private EcsFilter<MoveComponent, PauseTag> _pausedFilter;
        private EcsFilter<AvatarHitEvent> _hitFilter;
        private EcsFilter<ResumeAfterHitEvent> _resumeFilter;
        public void RunLate()
        {
            if (_hitFilter.GetEntitiesCount() > 0)
            {
                UnityEngine.Debug.Log("HIT!");
                foreach (var i in _filter)
                {
                    ref var move = ref _filter.Get1(i);
                    if (move.canBePaused)
                        _filter.GetEntity(i).Replace(new PauseTag());
                }
            }

            if (_resumeFilter.GetEntitiesCount() > 0)
            {
                UnityEngine.Debug.Log("Resume move!");
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