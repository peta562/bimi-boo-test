using Game;
using UnityEngine;

namespace Configs {
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject {
        public GameCanvas GameCanvasPrefab;
    }
}