using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Time;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.GameOver
{
    [EcsSystem(typeof(CoreModule))]
    public class PauseOnGameOverSystem : IEcsRunLateSystem
    {
        private EcsFilter<MoveComponent> _moveFilter;
        private EcsFilter<TimeComponent> _timeFilter;
        private EcsFilter<GameOverEvent> _gameOverFilter;

        public void RunLate()
        {
            if (_gameOverFilter.GetEntitiesCount() == 0)
                return;

            foreach (var i in _moveFilter)
            {
                _moveFilter.GetEntity(i).Replace(new PauseTag());
            }
            
            foreach (var i in _timeFilter)
            {
                ref var time = ref _timeFilter.Get1(i);
                time.factor = 0;
            }
        }
    }
}