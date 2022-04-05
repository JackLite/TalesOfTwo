using EcsCore;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace FlyAdventure.Core.Environment
{
    [EcsSystem(typeof(CoreModule))]
    public class TextureOffsetSystem : IEcsInitSystem
    {
        private EcsFilter<TextureOffsetComponent> _filter;
        private static readonly int speed = Shader.PropertyToID("_Speed");
        private static readonly int isRight = Shader.PropertyToID("_IsRight");
        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var toc = ref _filter.Get1(i);
                var mat = toc.spriteRenderer.material;
                mat.SetFloat(speed, toc.speed);
                mat.SetFloat(isRight, toc.isRight ? 1 : 0);
            }
        }
    }
}