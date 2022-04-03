using EcsCore;
using FlyAdventure.Core;
using Leopotam.Ecs;

namespace FlyAdventure.MainMenu
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