using EcsCore;
using FlyAdventure.Core.Collision;
using FlyAdventure.Core.Input;
using FlyAdventure.Core.Lines;
using FlyAdventure.Core.Move;
using FlyAdventure.Core.Move.ToLine;
using FlyAdventure.Core.Settings;
using Leopotam.Ecs;
using UnityEngine;

namespace FlyAdventure.Core.Avatars
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

        public void PreInit()
        {
            Create(_avatarsContainer.LeftAvatar);
            Create(_avatarsContainer.RightAvatar, true);
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
            _world.NewEntity()
                  .Replace(new AvatarTag())
                  .Replace(new InputComponent { isRight = isRight })
                  .Replace(new MoveComponent { view = avatar.MoveView, speed = _moveSettings.AvatarSpeed })
                  .Replace(new TransformComponent { transform = avatar.transform })
                  .Replace(moveToLine)
                  .Replace(new CollisionComponent { collider = avatar.Collision });
            avatar.SetRun();
            avatar.MoveView.SetY(CalculatePositionService.CalculateYForAvatar(10, startLine));
        }
    }
}