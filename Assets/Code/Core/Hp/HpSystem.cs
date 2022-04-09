using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Collision;
using TheTalesOfTwo.Core.Move;

namespace TheTalesOfTwo.Core.Hp
{
    [EcsSystem(typeof(CoreModule))]
    public class HpSystem : IEcsRunSystem
    {
        private EcsFilter<AvatarComponent, HpComponent> _hpFilter;
        private EcsFilter<AvatarComponent, CollideTag> _collideFilter;
        private EcsFilter<AvatarComponent> _avatars;
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
                ref var avatar = ref _collideFilter.Get1(i);
                avatar.view.SetHurt();
            }
        }
    }
}