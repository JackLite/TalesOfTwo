using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Settings;
using TheTalesOfTwo.Core.Time;

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
            Create(textureOffsetComponent);
        }

        private void CreateRight()
        {
            var textureOffsetComponent = new TextureOffsetComponent
            {
                isRight = true,
                speed = _settings.EnvironmentSpeed,
                spriteRenderer = _container.Right
            };
            Create(textureOffsetComponent);
        }
        private void Create(in TextureOffsetComponent textureOffsetComponent)
        {
            _ecsWorld.NewEntity().Replace(textureOffsetComponent).Replace(new TimeComponent { factor = 1 });
        }
    }
}