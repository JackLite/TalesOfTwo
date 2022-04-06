using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Settings;

namespace TheTalesOfTwo.Core.Hp.GUI
{
    [EcsSystem(typeof(CoreModule))]
    public class HpBarSystem : IEcsInitSystem, IEcsRunLateSystem
    {
        private CoreSettings _coreSettings;
        private HpBarView _hpBar;
        private EcsFilter<AvatarTag, HpComponent> _filter;

        public void Init()
        {
            for (var i = 0; i < _coreSettings.Health; i++)
                _hpBar.CreateOneHp();
        }

        public void RunLate()
        {
            foreach (var i in _filter)
            {
                ref var hp = ref _filter.Get2(i);
                _hpBar.UpdateHp(hp.currentHp);
            }
        }
    }
}