using System.Collections.Generic;
using Configs;
using Infrastructure;
using UnityEngine;

namespace Game {
    public sealed class LevelController {
        readonly List<SlotDescription> _targetSlots = new List<SlotDescription>();

        readonly SlotsConfig _slotsConfig;

        public LevelController(SlotsConfig slotsConfig) {
            _slotsConfig = slotsConfig;
        }

        public void StartLevel() {
            foreach (var slotDescription in _slotsConfig.SlotDescriptions) {
                _targetSlots.Add(slotDescription);
            }
        }

        public SlotDescription GetNextRandomTargetSlot() {
            var randomIndex = Random.Range(0, _targetSlots.Count - 1);

            return _targetSlots[randomIndex];
        }

        public void RemoveTargetSlot(SlotType slotType) {
            for (var i = 0; i < _targetSlots.Count; i++) {
                if ( _targetSlots[i].SlotType == slotType ) {
                    _targetSlots.Remove(_targetSlots[i]);
                }
            }
        }

        public bool CheckForWin() {
            return _targetSlots.Count == 0;
        }
    }
}