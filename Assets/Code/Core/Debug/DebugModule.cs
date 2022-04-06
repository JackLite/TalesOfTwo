using System.Threading.Tasks;
using TheTalesOfTwo.Core.Settings;
using UnityEngine.AddressableAssets;

namespace TheTalesOfTwo.Core.Debug
{
    public class DebugModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            var settings = await Addressables.LoadAssetAsync<CoreSettingsContainer>("CoreSettings").Task;
            Dependencies[typeof(MoveSettings)] = settings.MoveSettings;
            Dependencies[typeof(CoreSettings)] = settings.CoreSettings;
        }
    }
}