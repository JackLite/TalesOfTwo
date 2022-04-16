using System.Threading.Tasks;
using EcsCore;
using Leopotam.Ecs;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace TheTalesOfTwo.MainMenu
{
    public class MainMenuModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            //TODO: load stuff
            if (SceneManager.GetActiveScene().name != "Start")
                await Addressables.LoadSceneAsync("StartScene").Task;
            var mainScreen = await Addressables.InstantiateAsync("MainMenu").Task;
            Dependencies[typeof(MainScreenView)] = mainScreen.GetComponent<MainScreenView>();
            EcsWorldContainer.World.NewEntity().Replace(new MainMenuLoadTag());
        }
    }
}