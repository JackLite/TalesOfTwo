using EcsCore;
using Leopotam.Ecs;

namespace TheTalesOfTwo.Core.Pause
{
    [EcsSystem(typeof(CoreModule))]
    public class UnPauseSystem : IEcsRunSystem
    {
        private EcsFilter<UnpauseComponent> _filter;
        private EcsWorld _world;
        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
                return;
            
            if(_filter.GetEntitiesCount() > 1)
                UnityEngine.Debug.LogWarning($"For some reasons {typeof(UnpauseComponent)} more then one!");

            ref var unpause = ref _filter.Get1(0);
            if (unpause.unpauseTime <= UnityEngine.Time.time)
            {
                _filter.GetEntity(0).Destroy();
                _world.CreateOneFrame().Replace(new UnpauseEvent());
            }
        }
    }
}