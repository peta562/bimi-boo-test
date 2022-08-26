using System;
using System.Collections.Generic;
using Game;
using Game.Slots;
using UnityEngine;

namespace Configs {
    [Serializable]
    public class SlotDescription {
        public SlotType SlotType;
        public Sprite ItemSprite;
        public Sprite BackSprite;
    }

    [CreateAssetMenu(fileName = "SlotsConfig", menuName = "Configs/SlotsConfig", order = 2)]
    public sealed class SlotsConfig : ScriptableObject {
        public SlotsField SlotsFieldPrefab;
        public List<SlotDescription> SlotDescriptions;
    }
}