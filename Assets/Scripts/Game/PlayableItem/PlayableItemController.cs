using Configs;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SoundManager;
using UnityEngine;

namespace Game.PlayableItem {
    public sealed class PlayableItemController {
        readonly IGameFactory _gameFactory;
        readonly ISoundManager _soundManager;
        readonly PlayableItemConfig _playableItemConfig;

        PlayableItem _playableItem;

        public PlayableItemController(IGameFactory gameFactory, ISoundManager soundManager,
            PlayableItemConfig playableItemConfig) {
            _gameFactory = gameFactory;
            _soundManager = soundManager;
            _playableItemConfig = playableItemConfig;
        }

        public void InitItem(Canvas mainCanvas, Transform playableItemRoot) {
            _playableItem = _gameFactory.CreateGameObject(_playableItemConfig.PlayableItemPrefab, playableItemRoot);
            _playableItem.Init(mainCanvas, _soundManager);
        }

        public void ChangeItemType(SlotType slotType, Sprite itemSprite) {
            _playableItem.ChangeType(slotType, itemSprite);

            _playableItem.PlayShowAnimationAndSound();
        }

        public void ResetItem() {
            _playableItem.ResetItem();
        }
    }
}