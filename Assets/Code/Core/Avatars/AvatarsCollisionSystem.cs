using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Collision;
using TheTalesOfTwo.Core.Environment;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Misc;

namespace TheTalesOfTwo.Core.Avatars
{
    [EcsSystem(typeof(CoreModule))]
    public class AvatarsCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarComponent, CollisionComponent> _filter;
        private EcsFilter<MoveComponent> _moveFilter;
        private EcsFilter<TextureOffsetComponent> _textureOffset;
        private EcsWorld _world;

        public void Run()
        {
            var isCollide = false;
            foreach (var i in _filter)
            {
                ref var collision = ref _filter.Get2(i);
                if (!collision.isCollide)
                    continue;

                collision.isCollide = false;
                collision.collider.StopUntilExit();
                UnityEngine.Debug.Log("Collide!");
                _filter.GetEntity(i).Replace(new CollideTag());
                isCollide = true;
            }

            if (isCollide)
                _world.CreateOneFrame().Replace(new AvatarHitEvent());
        }
    }
}