using System.Threading.Tasks;
using EcsCore;
using TheTalesOfTwo.Core;
using TheTalesOfTwo.StartScreen;
using UnityEngine.SceneManagement;

namespace TheTalesOfTwo
{
    [EcsGlobalModule]
    public class MainModule : EcsModule
    {
        protected override Task Setup()
        {
            if (SceneManager.GetActiveScene().name == "Core")
                EcsWorldContainer.World.ActivateModule<CoreModule>();
            else
                EcsWorldContainer.World.ActivateModule<StartScreenModule>();

            return Task.CompletedTask;
        }
    }
}