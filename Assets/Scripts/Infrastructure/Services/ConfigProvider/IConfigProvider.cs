using UnityEngine;

namespace Infrastructure.Services.ConfigProvider {
    public interface IConfigProvider : IService {
        T LoadConfig<T>(string path) where T : ScriptableObject;
    }
}