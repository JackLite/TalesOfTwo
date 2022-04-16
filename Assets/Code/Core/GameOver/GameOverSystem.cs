using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Hp;

namespace TheTalesOfTwo.Core.GameOver
{
    [EcsSystem(typeof(CoreModule))]
    public class GameOverSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarTag, HpComponent> _filter;
        private EcsWorld _world;
        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0)
                return;
            
            ref var hp = ref _filter.Get2(0);
            if (hp.currentHp > 0)
                return;

            _world.CreateOneFrame().Replace(new GameOverEvent());
        }
    }
}