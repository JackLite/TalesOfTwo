using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Settings;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.AvatarHit
{
    [EcsSystem(typeof(CoreModule))]
    public class ResumeAfterHitSystem : IEcsRunSystem, IEcsRunLateSystem
    {
        private EcsFilter<ResumeAfterHitComponent> _resumeFilter;
        private EcsFilter<AvatarHitEvent> _hitFilter;
        private EcsFilter<UnpauseEvent> _unpauseFilter;
        private EcsFilter<TimeComponent> _timeFilter;
        private EcsFilter<MoveComponent>.Exclude<AvatarViewComponent> _moveFilter;
        private CoreSettings _settings;
        private EcsWorld _world;

        public void Run()
        {
            if (_resumeFilter.GetEntitiesCount() == 0)
                return;

            if (_hitFilter.GetEntitiesCount() > 0)
            {
                DeleteResume();
                return;
            }

            if (_resumeFilter.GetEntitiesCount() > 1)
            {
                _resumeFilter.GetEntity(1).Destroy();
                UnityEngine.Debug.LogError("Resume after hit must be one!");
            }

            ProcessResume();
        }

        public void RunLate()
        {
            if (_unpauseFilter.GetEntitiesCount() == 0)
                return;
            _world.NewEntity()
                  .Replace(new ResumeAfterHitComponent
                      { duration = _settings.HitResumeTime, remain = _settings.HitResumeTime })
                  .Replace(new CleanUpTag());
        }

        private void DeleteResume()
        {
            foreach (var i in _resumeFilter)
                _resumeFilter.GetEntity(i).Destroy();
        }
        private void ProcessResume()
        {
            ref var resume = ref _resumeFilter.Get1(0);

            resume.remain -= UnityEngine.Time.deltaTime;
            if (resume.remain <= 0)
                DeleteResume();

            var factor = (resume.duration - resume.remain) / resume.duration;
            foreach (var i in _timeFilter)
            {
                ref var time = ref _timeFilter.Get1(i);
                time.factor = factor;
            }

            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get1(i);
                if (move.canBePaused)
                    move.factor = factor;
            }
        }


    }
}