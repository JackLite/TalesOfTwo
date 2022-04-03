using EcsCore;
using Leopotam.Ecs;

namespace FlyAdventure.Core.Collision
{
    [EcsSystem(typeof(CoreModule))]
    public class CollisionSystem : IEcsRunLateSystem
    {
        private EcsFilter<CollisionComponent> _filter;
        public void RunLate()
        {
            foreach (var i in _filter)
            {
                ref var collision = ref _filter.Get1(i);
                collision.isCollide = collision.collider.IsCollide();
            }
        }
    }
}