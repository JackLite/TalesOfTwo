using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Time;
using TheTalesOfTwo.Misc;
using UnityEngine;

namespace TheTalesOfTwo.Core.Environment
{
    [EcsSystem(typeof(CoreModule))]
    public class TextureOffsetSystem : IEcsRunSystem
    {
        private EcsFilter<TextureOffsetComponent, TimeComponent>.Exclude<PauseTag> _filter;
        private static readonly int texOffsetX = Shader.PropertyToID("_TexOffsetX");

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var toc = ref _filter.Get1(i);
                ref var time = ref _filter.Get2(i);
                var x = toc.speed * time.factor * time.deltaTime * (toc.isRight ? -1 : 1);
                toc.offset += x;
                toc.offset %= toc.spriteRenderer.size.x;
                toc.spriteRenderer.material.SetFloat(texOffsetX, toc.offset);
            }
        }
    }
}