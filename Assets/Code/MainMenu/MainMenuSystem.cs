using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core;

namespace TheTalesOfTwo.MainMenu
{
    [EcsSystem(typeof(MainMenuModule))]
    public class MainMenuSystem : IEcsInitSystem
    {
        private MainScreenView _mainScreen;
        private EcsWorld _world;
        public void Init()
        {
            _mainScreen.OnStart += StartCore;
        }

        private void StartCore()
        {
            _world.ActivateModule<CoreModule>();
            _mainScreen.Destroy();
            _world.DeactivateModule<MainModule>();
        }
    }
}