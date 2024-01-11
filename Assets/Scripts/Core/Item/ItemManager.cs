using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Item
{
    public class ItemManager : IItemManager
    {
        [Inject]
        private DiContainer _diContainer;
        
        private List<ItemBehaviour> _items = new();

        private ReactiveProperty<int> _collectedItems;
        private ReactiveProperty<int> _allItems;

        public IReadOnlyReactiveProperty<int> CollectedItems => _collectedItems;
        public IReadOnlyReactiveProperty<int> AllItems => _allItems;

        public void RegisterSpawn(
            Vector3 position,
            Quaternion quaternion,
            ItemConfig config,
            Transform parent)
        {
            var newPrefab = _diContainer.InstantiatePrefabForComponent<ItemBehaviour>(config.prefab);

            var prefabTransform = newPrefab.transform;
            
            prefabTransform.position = position;
            prefabTransform.rotation = quaternion;
            
            _items.Add(newPrefab);
        }

        public void RegisterCollect(ItemBehaviour item)
        {
            _collectedItems.Value++;
            
            _items.Remove(item);
            Object.Destroy(item.gameObject);
        }
    }
}