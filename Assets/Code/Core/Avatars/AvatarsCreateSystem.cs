using EcsCore;
using Leopotam.Ecs;
using TheTalesOfTwo.Core.Collision;
using TheTalesOfTwo.Core.Hp;
using TheTalesOfTwo.Core.Input;
using TheTalesOfTwo.Core.Lines;
using TheTalesOfTwo.Core.Move;
using TheTalesOfTwo.Core.Move.ToLine;
using TheTalesOfTwo.Core.Settings;
using UnityEngine;

namespace TheTalesOfTwo.Core.Avatars
{
    /// <summary>
    /// Отвечает за создание аватаров игрока
    /// </summary>
    [EcsSystem(typeof(CoreModule))]
    public class AvatarsCreateSystem : IEcsPreInitSystem
    {
        private EcsWorld _world;
        private AvatarsContainer _avatarsContainer;
        private MoveSettings _moveSettings;
        private CoreSettings _coreSettings;

        public void PreInit()
        {
            Create(_avatarsContainer.LeftAvatar);
            Create(_avatarsContainer.RightAvatar, true);

            _world.NewEntity().Replace(new HpComponent(_coreSettings.Health)).Replace(new AvatarComponent());
        }

        private void Create(AvatarView avatar, bool isRight = false)
        {
            var lines = 3f;
            var startLine = Mathf.CeilToInt(lines / 2);
            var moveToLine = new MoveToLine
            {
                fromLine = startLine,
                toLine = startLine,
                ease = _moveSettings.ease
            };
            var moveComponent = new MoveComponent
            {
                view = avatar.MoveView, 
                speed = _moveSettings.AvatarSpeed, 
                canBePaused = true,
                factor = 1
            };
            _world.NewEntity()
                  .Replace(moveComponent)
                  .Replace(moveToLine)
                  .Replace(new AvatarComponent { view = avatar })
                  .Replace(new InputComponent { isRight = isRight })
                  .Replace(new TransformComponent { transform = avatar.transform })
                  .Replace(new CollisionComponent { collider = avatar.Collision });
            avatar.SetRun();
            avatar.MoveView.SetY(CalculatePositionService.CalculateYForAvatar(10, startLine));
        }
    }
}