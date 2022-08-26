using System;
using System.Collections.Generic;
using Configs;
using Extensions;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SoundManager;
using UnityEngine;

namespace Game.Slots {
    public sealed class SlotsFieldController {
        readonly IGameFactory _gameFactory;
        readonly ISoundManager _soundManager;
        readonly SlotsConfig _slotsConfig;

        SlotsField _slotsField;

        public SlotsFieldController(IGameFactory gameFactory, ISoundManager soundManager, SlotsConfig slotsConfig) {
            _gameFactory = gameFactory;
            _soundManager = soundManager;
            _slotsConfig = slotsConfig;
        }

        public void CreateSlots(Transform slotFieldRoot, Action onCreated, Action<SlotType> onSlotCompleted) {
            _slotsField = _gameFactory.CreateGameObject(_slotsConfig.SlotsFieldPrefab, slotFieldRoot);

            var shuffledSlotsDescriptions = ShuffleSlotDescriptions(_slotsConfig.SlotDescriptions);
            _slotsField.InitSlotsField(_soundManager, shuffledSlotsDescriptions, onSlotCompleted);

            PlaySlotsShowAnimationAndSound(onCreated);
        }

        public void DisableSlot(SlotType slotType) {
            _slotsField.DisableSlot(slotType);
        }

        void PlaySlotsShowAnimationAndSound(Action onCreated) {
            _slotsField.PlaySlotsShowAnimationAndSound(onCreated);
        }

        List<SlotDescription> ShuffleSlotDescriptions(List<SlotDescription> slotDescriptions) {
            var shuffledSlotsDescriptions = new List<SlotDescription>();

            foreach (var slotDescription in _slotsConfig.SlotDescriptions) {
                shuffledSlotsDescriptions.Add(slotDescription);
                shuffledSlotsDescriptions.Shuffle();
            }

            return shuffledSlotsDescriptions;
        }
    }
}