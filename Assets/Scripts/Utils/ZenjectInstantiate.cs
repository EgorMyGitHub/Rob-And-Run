using UnityEngine;
using Zenject;

namespace Utils
{
    public class ZenjectInstantiate
    {
        private static DiContainer _container;

        public static T SpawnFromPool<T>(
            MonoMemoryPool<T> pool,
            SpawnInfo? spawnInfo = null)
            where T: Component
        {
            var newObj = pool.Spawn();

            var transform = newObj.transform;

            if (spawnInfo.HasValue)
            {
                var info = spawnInfo.Value;
                
                transform.position = info.Position;
                transform.rotation = info.Rotation;
                transform.parent = info.Parent;
            }
            
            return newObj;
        }
        
        public static T SpawnFromFactory<T>(
            PlaceholderFactory<T> pool,
            SpawnInfo? spawnInfo = null)
            where T: Component
        {
            var newObj = pool.Create();

            var transform = newObj.transform;

            if (spawnInfo.HasValue)
            {
                var info = spawnInfo.Value;
                
                transform.position = info.Position;
                transform.rotation = info.Rotation;
                transform.parent = info.Parent;
            }
            
            return newObj;
        }

        public static T InstantiatePrefabForComponent<T>(
            T prefab,
            SpawnInfo? spawnInfo = null)
            where T: Component
        {
            var newObj = _container.InstantiatePrefabForComponent<T>(prefab);
            
            var transform = newObj.transform;

            if (spawnInfo.HasValue)
            {
                var info = spawnInfo.Value;
                
                transform.position = info.Position;
                transform.rotation = info.Rotation;
                transform.parent = info.Parent;
            }
            
            return newObj;
        }

        [Inject]
        private void Initialize(DiContainer container)
        {
            _container = container;
        }
    }
}
