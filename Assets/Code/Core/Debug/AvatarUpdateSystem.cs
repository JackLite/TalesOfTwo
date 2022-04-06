using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Move.ToLine;
using TheTalesOfTwo.Core.Settings;

namespace TheTalesOfTwo.Core.Debug
{
    [EcsSystem(typeof(DebugModule))]
    public class AvatarUpdateSystem : IEcsRunSystem
    {
        private MoveSettings _moveSettings;
        private EcsFilter<MoveComponent, MoveToLine> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var m = ref _filter.Get1(i);
                m.speed = _moveSettings.AvatarSpeed;
                ref var mtl = ref _filter.Get2(i);
                mtl.ease = _moveSettings.ease;
            }
        }
    }
}