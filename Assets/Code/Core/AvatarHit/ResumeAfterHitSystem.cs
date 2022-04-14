using System;
using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Pause;
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
        private EcsFilter<MoveComponent>.Exclude<AvatarComponent> _moveFilter;
        private EcsWorld _world;
        public void Run()
        {
            if (_resumeFilter.GetEntitiesCount() == 0)
                return;

            if (_hitFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _resumeFilter)
                    _resumeFilter.GetEntity(i).Destroy();
                return;
            }
            
            if (_resumeFilter.GetEntitiesCount() > 1)
            {
                _resumeFilter.GetEntity(1).Destroy();
                UnityEngine.Debug.LogError("Resume after hit must be one!");
            }

            ref var resume = ref _resumeFilter.Get1(0);
            ProcessResume(ref resume);
        }
        private void ProcessResume(ref ResumeAfterHitComponent resume)
        {
            if (Math.Abs(resume.remain - resume.duration) < 0.00001f)
            {
                _world.CreateOneFrame().Replace(new ResumeAfterHitEvent());
            }
            resume.remain -= UnityEngine.Time.deltaTime;
            if (resume.remain < 0)
            {
                resume.remain = 0;
                _resumeFilter.GetEntity(0).Replace(new EcsOneFrame());
            }
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
        public void RunLate()
        {
            if (_unpauseFilter.GetEntitiesCount() == 0)
                return;
            // TODO: брать время и прочее из настроек

            _world.NewEntity().Replace(new ResumeAfterHitComponent { duration = 4, remain = 4 });
        }
    }
}