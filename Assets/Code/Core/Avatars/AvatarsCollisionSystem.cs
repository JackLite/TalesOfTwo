using EcsCore;
using FlyAdventure.Core.Collision;
using Leopotam.Ecs;

namespace FlyAdventure.Core.Avatars
{
    [EcsSystem(typeof(CoreModule))]
    public class AvatarsCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarTag, CollisionComponent> _filter;
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
            }
        }
    }
}