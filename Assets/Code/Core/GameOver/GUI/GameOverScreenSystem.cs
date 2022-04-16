using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.MainMenu;
using TheTalesOfTwo.StartScreen;

namespace TheTalesOfTwo.Core.GameOver.GUI
{
    [EcsSystem(typeof(CoreModule))]
    public class GameOverScreenSystem : IEcsInitSystem, IEcsRunLateSystem
    {
        private EcsFilter<GameOverEvent> _eventFilter;
        private GameOverScreenView _gameOverScreen;
        private EcsWorld _world;
        public void Init()
        {
            _gameOverScreen.OnReturnToMenu += OnReturnToMenu;
        }
        public void RunLate()
        {
            if (_eventFilter.GetEntitiesCount() == 0)
                return;
            
            _gameOverScreen.gameObject.SetActive(true);
        }

        private void OnReturnToMenu()
        {
            _world.DeactivateModule<CoreModule>();
            _world.ActivateModule<MainMenuModule>();
        }
    }
}