using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.AvatarHit;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Misc;
using UnityEngine;

namespace TheTalesOfTwo.Core.Environment
{
    [EcsSystem(typeof(CoreModule))]
    public class TextureOffsetSystem : IEcsInitSystem, IEcsRunSystem, IEcsRunLateSystem
    {
        private EcsFilter<TextureOffsetComponent> _filter;
        private EcsFilter<TextureOffsetComponent, PauseTag> _pausedFilter;
        private EcsFilter<TextureOffsetComponent, UnPauseTag> _unPausedFilter;
        private EcsFilter<AvatarHitEvent> _hitFilter;
        private static readonly int speed = Shader.PropertyToID("_Speed");
        private static readonly int isRight = Shader.PropertyToID("_IsRight");
        private static readonly int isPause = Shader.PropertyToID("_IsPause");

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var toc = ref _filter.Get1(i);
                var mat = toc.spriteRenderer.material;
                mat.SetFloat(speed, toc.speed);
                mat.SetFloat(isRight, toc.isRight ? 1 : 0);
                mat.SetFloat(isPause, 0);
            }
        }

        public void Run()
        {
            foreach (var i in _pausedFilter)
            {
                ref var toc = ref _pausedFilter.Get1(i);
                toc.spriteRenderer.material.SetFloat(isPause, 1);
                _pausedFilter.GetEntity(i).Del<PauseTag>();
            }

            foreach (var i in _unPausedFilter)
            {
                ref var toc = ref _unPausedFilter.Get1(i);
                toc.spriteRenderer.material.SetFloat(isPause, 0);
                _unPausedFilter.GetEntity(i).Del<UnPauseTag>();
            }
        }

        public void RunLate()
        {
            if (_hitFilter.GetEntitiesCount() > 0)
            {
                foreach (var i in _filter)
                {
                    ref var toc = ref _filter.Get1(i);
                    toc.spriteRenderer.material.SetFloat(isPause, 1);
                }
            }
        }
    }
}