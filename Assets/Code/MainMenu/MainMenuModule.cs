using System.Threading.Tasks;
using EcsCore;
using Leopotam.Ecs;
using UnityEngine.AddressableAssets;

namespace FlyAdventure.MainMenu
{
    public class MainMenuModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            //TODO: load stuff
            var mainScreen = await Addressables.InstantiateAsync("MainMenu").Task;
            Dependencies[typeof(MainScreenView)] = mainScreen.GetComponent<MainScreenView>();
            EcsWorldContainer.World.NewEntity().Replace(new MainMenuLoadTag());
        }
    }
}