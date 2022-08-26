using System;
using Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.GameFactory {
    public sealed class GameFactory : IGameFactory {
        public T CreateGameObject<T>(T prefab, Transform transform = null) where T : Component {
            var gameObject = Object.Instantiate(prefab, transform);

            if ( gameObject == null ) {
                throw new NullReferenceException($"Component {typeof(T)} is null");
            }

            return gameObject;
        }
    }
}