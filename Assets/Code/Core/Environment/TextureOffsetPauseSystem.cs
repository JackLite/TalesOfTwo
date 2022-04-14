using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.AvatarHit;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Time;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Environment
{
    [EcsSystem(typeof(CoreModule))]
    public class TextureOffsetPauseSystem : IEcsRunLateSystem
    {
        private EcsFilter<TextureOffsetComponent, TimeComponent> _filter;
        private EcsFilter<PauseEvent> _pauseFilter;
        private EcsFilter<UnpauseEvent> _unpauseFilter;
        public void RunLate()
        {
            CheckHit();
            CheckResumeAfterHit();
        }

        private void CheckHit()
        {
            if (_pauseFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _filter)
                {
                    _filter.GetEntity(i).Replace(new PauseTag());
                }
            }
        }

        private void CheckResumeAfterHit()
        {
            if (_unpauseFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _filter)
                {
                    _filter.GetEntity(i).Del<PauseTag>();
                }
            }
        }
    }
}