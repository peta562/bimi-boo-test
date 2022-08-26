using Game.PlayableItem;
using Game.Slots;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SoundManager;

namespace Game {
    public sealed class GameCycle {
        readonly SlotsFieldController _slotsFieldController;
        readonly PlayableItemController _playableItemController;
        readonly LevelController _levelController;

        readonly GameCanvas _gameCanvas;

        public GameCycle(ConfigsHolder configsHolder, ServiceLocator services) {
            var gameFactory = services.Single<IGameFactory>();
            _gameCanvas = gameFactory.CreateGameObject(configsHolder.GameConfig.GameCanvasPrefab);

            _levelController = new LevelController(configsHolder.SlotsConfig);
            _slotsFieldController =
                new SlotsFieldController(gameFactory, services.Single<ISoundManager>(), configsHolder.SlotsConfig);
            _playableItemController = new PlayableItemController(gameFactory, services.Single<ISoundManager>(),
                configsHolder.PlayableItemConfig);
        }


        public void StartGame() {
            _levelController.StartLevel();
            _slotsFieldController.CreateSlots(_gameCanvas.SlotFieldRoot, onCreated: CreatePlayableItem,
                onSlotCompleted: OnSlotComplete);
        }

        void CreatePlayableItem() {
            _playableItemController.InitItem(_gameCanvas.MainCanvas, _gameCanvas.PlayableItemRoot);
            ChangePlayableItemType();
        }

        void OnSlotComplete(SlotType slotType) {
            _playableItemController.ResetItem();
            _slotsFieldController.DisableSlot(slotType);
            _levelController.RemoveTargetSlot(slotType);

            if ( _levelController.CheckForWin() ) {
                return;
            }

            ChangePlayableItemType();
        }

        void ChangePlayableItemType() {
            var nextTargetSlot = _levelController.GetNextRandomTargetSlot();
            _playableItemController.ChangeItemType(nextTargetSlot.SlotType, nextTargetSlot.ItemSprite);
        }
    }
}