using System;
using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.AvatarHit
{
    [EcsSystem(typeof(CoreModule))]
    public class ResumeAfterHitSystem : IEcsRunSystem
    {
        private EcsFilter<ResumeAfterHitComponent> _resumeFilter;
        private EcsFilter<TimeComponent> _timeFilter;
        private EcsFilter<MoveComponent>.Exclude<AvatarComponent> _moveFilter;
        private EcsWorld _world;
        public void Run()
        {
            if (_resumeFilter.GetEntitiesCount() <= 0)
                return;

            if (_resumeFilter.GetEntitiesCount() > 1)
            {
                _resumeFilter.GetEntity(1).Destroy();
                // TODO: слишком долго живет компонент resume, нужно разбить процесс на этапы отдельные
                throw new Exception("Resume after hit must be one!");
            }

            ref var resume = ref _resumeFilter.Get1(0);

            resume.delay -= UnityEngine.Time.deltaTime;
            if (resume.delay <= 0)
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
    }
}