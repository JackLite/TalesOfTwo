using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Settings;

namespace TheTalesOfTwo.Core.Environment
{
    [EcsSystem(typeof(CoreModule))]
    public class EnvironmentCreateSystem : IEcsPreInitSystem
    {
        private EnvironmentContainer _container;
        private EcsWorld _ecsWorld;
        private CoreSettings _settings;

        public void PreInit()
        {
            CreateLeft();
            CreateRight();
        }

        private void CreateLeft()
        {
            var textureOffsetComponent = new TextureOffsetComponent
            {
                isRight = false,
                speed = _settings.EnvironmentSpeed,
                spriteRenderer = _container.Left
            };
            _ecsWorld.NewEntity().Replace(textureOffsetComponent);
        }

        private void CreateRight()
        {
            var textureOffsetComponent = new TextureOffsetComponent
            {
                isRight = true,
                speed = _settings.EnvironmentSpeed,
                spriteRenderer = _container.Right
            };
            _ecsWorld.NewEntity().Replace(textureOffsetComponent);
        }
    }
}