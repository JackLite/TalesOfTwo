using System.Threading.Tasks;
using EcsCore;
using FlyAdventure.Core;
using FlyAdventure.StartScreen;
using UnityEngine.SceneManagement;

namespace FlyAdventure
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