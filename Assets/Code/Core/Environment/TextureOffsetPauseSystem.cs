using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.AvatarHit;
using TheTalesOfTwo.Core.Time;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Environment
{
    [EcsSystem(typeof(CoreModule))]
    public class TextureOffsetPauseSystem : IEcsRunLateSystem
    {
        private EcsFilter<TextureOffsetComponent, TimeComponent> _filter;
        private EcsFilter<AvatarHitEvent> _hitFilter;
        private EcsFilter<ResumeAfterHitEvent> _resumeFilter;
        public void RunLate()
        {
            CheckHit();
            CheckResumeAfterHit();
        }

        private void CheckHit()
        {
            if (_hitFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _filter)
                {
                    _filter.GetEntity(i).Replace(new PauseTag());
                }
            }
        }

        private void CheckResumeAfterHit()
        {
            if (_resumeFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _filter)
                {
                    _filter.GetEntity(i).Del<PauseTag>();
                }
            }
        }
    }
}