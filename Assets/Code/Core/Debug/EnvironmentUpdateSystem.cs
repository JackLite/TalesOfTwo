using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Environment;
using TheTalesOfTwo.Core.Settings;
using UnityEngine;

namespace TheTalesOfTwo.Core.Debug
{
    [EcsSystem(typeof(DebugModule))]
    public class EnvironmentUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<TextureOffsetComponent> _filter;
        private CoreSettings _settings;
        private static readonly int speed = Shader.PropertyToID("_Speed");
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var toc = ref _filter.Get1(i);
                var mat = toc.spriteRenderer.material;
                mat.SetFloat(speed, _settings.EnvironmentSpeed);
            }
        }
    }
}