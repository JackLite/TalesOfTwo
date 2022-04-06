using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Collision;

namespace TheTalesOfTwo.Core.Hp
{
    [EcsSystem(typeof(CoreModule))]
    public class HpSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarTag, HpComponent> _hpFilter;
        private EcsFilter<AvatarTag, CollideTag> _collideFilter;
        public void Run()
        {
            foreach (var i in _collideFilter)
            {
                
                foreach (var j in _hpFilter)
                {
                    ref var hp = ref _hpFilter.Get2(j);
                    hp.currentHp--;
                }
                _collideFilter.GetEntity(i).Del<CollideTag>();
            }
        }
    }
}