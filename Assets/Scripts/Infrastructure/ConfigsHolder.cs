using Configs;
using Infrastructure.Services.ConfigProvider;

namespace Infrastructure {
    public sealed class ConfigsHolder {
        public GameConfig GameConfig { get; private set; }
        public SlotsConfig SlotsConfig { get; private set; }
        public PlayableItemConfig PlayableItemConfig { get; private set; }

        public void LoadConfigs(IConfigProvider configProvider) {
            GameConfig = configProvider.LoadConfig<GameConfig>("Configs/GameConfig");
            SlotsConfig = configProvider.LoadConfig<SlotsConfig>("Configs/SlotsConfig");
            PlayableItemConfig = configProvider.LoadConfig<PlayableItemConfig>("Configs/PlayableItemConfig");
        }
    }
}