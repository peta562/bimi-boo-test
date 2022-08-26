using UnityEngine;

namespace Infrastructure.Services.GameFactory {
    public interface IGameFactory : IService {
        T CreateGameObject<T>(T prefab, Transform transform = null) where T : Component;
    }
}