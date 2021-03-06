using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Collision;
using Unity.Mathematics;

namespace TheTalesOfTwo.Core.Hp
{
    [EcsSystem(typeof(CoreModule))]
    public class HpSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarTag, HpComponent> _hpFilter;
        private EcsFilter<AvatarViewComponent, CollideTag> _collideFilter;
        public void Run()
        {
            foreach (var i in _collideFilter)
            {
                foreach (var j in _hpFilter)
                {
                    ref var hp = ref _hpFilter.Get2(j);
                    hp.currentHp = math.max(hp.currentHp - 1, 0);
                }
                _collideFilter.GetEntity(i).Del<CollideTag>();
                ref var avatar = ref _collideFilter.Get1(i);
                avatar.view.SetHurt();
            }
        }
    }
}