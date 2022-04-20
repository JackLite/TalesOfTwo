using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.LevelProgress.GUI;
using TheTalesOfTwo.Core.Obstacles;
using TheTalesOfTwo.Core.Obstacles.Patterns;

namespace TheTalesOfTwo.Core.LevelProgress
{
    [EcsSystem(typeof(CoreModule))]
    public class LevelProgressSystem : IEcsRunLateSystem
    {
        private LevelProgressView _view;
        private EcsOneData<ObstaclesOneData> _obstaclesData;

        public void RunLate()
        {
            // get progress
            ref var obstacles = ref _obstaclesData.GetData();

            var progress = 1 - (obstacles.remainCount / (float) obstacles.totalCount);
            _view.SetProgress(progress);
        }
    }
}