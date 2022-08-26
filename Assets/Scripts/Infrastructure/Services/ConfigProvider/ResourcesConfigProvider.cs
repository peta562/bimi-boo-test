using System;
using UnityEngine;

namespace Infrastructure.Services.ConfigProvider {
    public sealed class ResourcesConfigProvider : IConfigProvider {
        public T LoadConfig<T>(string path) where T : ScriptableObject{
            var resource = Resources.Load<T>(path);

            if ( resource == null ) {
                throw new NullReferenceException($"Config at path :{path} is null");
            }

            return resource;
        }
    }
}