using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.MainMenu;

namespace TheTalesOfTwo.StartScreen
{
    [EcsSystem(typeof(StartScreenModule))]
    public class StartScreenSystem : IEcsRunSystem
    {
        private EcsFilter<MainMenuLoadTag> _destroyFilter;
        private EcsWorld _ecsWorld;
        private StartScreenView _startScreenView;

        public void Run()
        {
            if (_destroyFilter.GetEntitiesCount() > 0)
            {
                _startScreenView.Destroy();
                _destroyFilter.GetEntity(0).Replace(new EcsOneFrame());
                _ecsWorld.DeactivateModule<StartScreenModule>();
            }
        }
    }
}