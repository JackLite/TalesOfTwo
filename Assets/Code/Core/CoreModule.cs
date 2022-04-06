﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EcsCore;
using TheTalesOfTwo.Core.Avatars;
using TheTalesOfTwo.Core.Debug;
using TheTalesOfTwo.Core.Environment;
using TheTalesOfTwo.Core.Hp.GUI;
using TheTalesOfTwo.Core.Lines;
using TheTalesOfTwo.Core.Obstacles;
using TheTalesOfTwo.Core.Obstacles.Patterns;
using TheTalesOfTwo.Core.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TheTalesOfTwo.Core
{
    public class CoreModule : EcsModuleWithDependencies
    {
        private SceneInstance _scene;
        private readonly List<Object> _resources = new();
        private CoreSettingsContainer _settings;

        protected override async Task Setup()
        {
            if (SceneManager.GetActiveScene().name != "Core")
                _scene = await Addressables.LoadSceneAsync("CoreScene").Task;
            _settings = await Addressables.LoadAssetAsync<CoreSettingsContainer>("CoreSettings").Task;
            var patterns = await Addressables.LoadAssetAsync<TextAsset>("patterns").Task;
            _resources.Add(patterns);
            _resources.Add(_settings);
            await SetupView();
            await SetupGUI();
            Dependencies[typeof(PatternsRawContainer)] = new PatternsRawContainer(patterns.text);
            Dependencies[typeof(CoreSettings)] = _settings.CoreSettings;
            Dependencies[typeof(MoveSettings)] = _settings.MoveSettings;
            if (Application.isEditor)
                World.ActivateModule<DebugModule>();
        }
        
        private async Task SetupGUI()
        {
            var proxy = Object.FindObjectOfType<CoreGUIProxy>();
            Dependencies[typeof(HpBarView)] = proxy.HpBarView;
            await Task.CompletedTask;
        }

        private async Task SetupView()
        {
            var proxy = Object.FindObjectOfType<CoreViewProxy>();
            Dependencies[typeof(AvatarsContainer)] = proxy.AvatarsContainer;
            Dependencies[typeof(LinesPool)] = proxy.LinesPool;
            Dependencies[typeof(ObstaclesPool)] = proxy.ObstaclesPool;
            Dependencies[typeof(EnvironmentContainer)] = proxy.EnvironmentContainer;
            await proxy.LinesPool.WarmUp(3);      // TODO: брать кол-во линий из настроек уровня
            await proxy.ObstaclesPool.WarmUp(10); //TODO: 10 - число наугад, вынести в настройки уровня
        }

        public override void Deactivate()
        {
            if (Application.isEditor)
                World.DeactivateModule<DebugModule>();
            foreach (var resource in _resources)
                Addressables.Release(resource);
            Addressables.Release(_scene);
        }
    }
}