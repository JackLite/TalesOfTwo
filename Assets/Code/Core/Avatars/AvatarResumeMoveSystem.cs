using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.AvatarHit;
using TheTalesOfTwo.Core.Move;

namespace TheTalesOfTwo.Core.Avatars
{
    [EcsSystem(typeof(CoreModule))]
    public class AvatarResumeMoveSystem : IEcsRunLateSystem
    {
        private EcsFilter<ResumeAfterHitEvent> _resumeFilter;
        private EcsFilter<AvatarComponent, MoveComponent> _moveFilter;    
        public void RunLate()
        {
            if (_resumeFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get2(i);
                move.factor = 1;
            }
        }
    }
}