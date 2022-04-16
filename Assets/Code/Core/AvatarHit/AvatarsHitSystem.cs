using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Cleanup;
using TheTalesOfTwo.Core.Collision;
using TheTalesOfTwo.Core.Environment;
using TheTalesOfTwo.Core.Pause;

namespace TheTalesOfTwo.Core.AvatarHit
{
    [EcsSystem(typeof(CoreModule))]
    public class AvatarsHitSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarViewComponent, CollisionComponent> _filter;
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
            {
                _world.CreateOneFrame().Replace(new PauseEvent()).Replace(new AvatarHitEvent());
                // TODO: брать время и прочее из настроек
                _world.NewEntity()
                      .Replace(new UnpauseComponent { unpauseTime = UnityEngine.Time.time + 2 })
                      .Replace(new CleanUpTag());
            }
        }
    }
}