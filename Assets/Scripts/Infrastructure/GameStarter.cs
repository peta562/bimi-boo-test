using Game;
using Infrastructure.Services;
using Infrastructure.Services.ConfigProvider;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SoundManager;
using UnityEngine;

namespace Infrastructure {
    public sealed class GameStarter : MonoBehaviour {
        void Awake() {
            ConfigureBuildSettings();
            
            var services = new ServiceLocator();
            services.RegisterSingle<IConfigProvider>(new ResourcesConfigProvider());
            services.RegisterSingle<IGameFactory>(new GameFactory());
            services.RegisterSingle<ISoundManager>(new SoundManager());

            var configsHolder = new ConfigsHolder();
            configsHolder.LoadConfigs(services.Single<IConfigProvider>());

            var gameCycle = new GameCycle(configsHolder, services);
            gameCycle.StartGame();
        }

        void ConfigureBuildSettings() {
            Application.targetFrameRate = 60;
        }
    }
}