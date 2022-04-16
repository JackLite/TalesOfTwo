using EcsCore;
using Leopotam.Ecs;

namespace TheTalesOfTwo.Core.Cleanup
{
    [EcsSystem(typeof(CoreModule))]
    public class CleanUpSystem : IEcsDestroySystem
    {
        private EcsFilter<CleanUpTag> _filter;
        public void Destroy()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}