using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Collision;

namespace TheTalesOfTwo.Core.Avatars
{
    [EcsSystem(typeof(CoreModule))]
    public class AvatarsCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarTag, CollisionComponent> _filter;
        private EcsWorld _world;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var collision = ref _filter.Get2(i);
                if (!collision.isCollide)
                    continue;

                collision.isCollide = false;
                collision.collider.StopUntilExit();
                UnityEngine.Debug.Log("Collide!");
                _filter.GetEntity(i).Replace(new CollideTag());
            }
        }
    }
}