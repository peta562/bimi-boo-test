using Game.PlayableItem;
using UnityEngine;

namespace Configs {
    
    [CreateAssetMenu(fileName = "PlayableItemConfig", menuName = "Configs/PlayableItemConfig", order = 1)]
    public class PlayableItemConfig : ScriptableObject {
        public PlayableItem PlayableItemPrefab;
    }
}