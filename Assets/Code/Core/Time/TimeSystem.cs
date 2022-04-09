using EcsCore;
using Leopotam.Ecs;

namespace TheTalesOfTwo.Core.Time
{
    [EcsSystem(typeof(CoreModule))]
    public class TimeSystem : IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var time = ref _filter.Get1(i);
                time.deltaTime = UnityEngine.Time.deltaTime;
            }
        }
    }
}