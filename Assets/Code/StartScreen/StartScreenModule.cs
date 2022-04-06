using System.Collections.Generic;
using System.Threading.Tasks;
using EcsCore;
using TheTalesOfTwo.MainMenu;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace TheTalesOfTwo.StartScreen
{
    public class StartScreenModule : EcsModuleWithDependencies
    {
        private readonly List<Object> _resources = new();
        protected override async Task Setup()
        {
            var startScreen = await Addressables.InstantiateAsync("StartScreen").Task;
            _resources.Add(startScreen);
            Dependencies.Add(typeof(StartScreenView), startScreen.GetComponent<StartScreenView>());
            EcsWorldContainer.World.ActivateModule<MainMenuModule>();
        }

        public override void Deactivate()
        {
            foreach (var resource in _resources)
                Addressables.Release(resource);
        }
    }
}