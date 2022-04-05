using EcsCore;
using FlyAdventure.Core.Environment;
using FlyAdventure.Core.Settings;
using Leopotam.Ecs;
using UnityEngine;

namespace FlyAdventure.Core.Debug
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