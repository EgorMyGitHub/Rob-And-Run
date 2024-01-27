using UniRx;
using UnityEngine;

namespace Core.Item
{
    public interface IItemManager
    {
        IReadOnlyReactiveProperty<int> CollectedItems { get; }
        IReadOnlyReactiveProperty<int> AllItems { get; }
        
        void RegisterSpawn(
            Vector3 position,
            Quaternion quaternion,
            ItemBehaviour item,
            Transform parent);
        
        void RegisterCollect(ItemBehaviour item);
    }
}