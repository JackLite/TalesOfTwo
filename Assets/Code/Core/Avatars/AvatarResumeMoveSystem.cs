using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Pause;
using TheTalesOfTwo.Core.Time;

namespace TheTalesOfTwo.Core.Avatars
{
    [EcsSystem(typeof(CoreModule))]
    public class AvatarResumeMoveSystem : IEcsRunSystem, IEcsRunLateSystem
    {
        private EcsFilter<UnpauseEvent> _unpauseFilter;
        private EcsFilter<AvatarComponent, MoveComponent, TimeComponent> _moveFilter;    
        public void RunLate()
        {
            if (_unpauseFilter.GetEntitiesCount() <= 0)
                return;

            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get2(i);
                move.factor = 1;
                ref var avatar = ref _moveFilter.Get1(i);
                avatar.view.SetRun();
            }
        }
        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var avatar = ref _moveFilter.Get1(i);
                ref var time = ref _moveFilter.Get3(i);
                avatar.view.SetRunSpeed(time.factor);
            }
        }
    }
}