using UnityEngine;

namespace Core.Item
{
    public interface IItemManager
    {
        public void RegisterSpawn(
            Vector3 position,
            Quaternion quaternion,
            ItemConfig config,
            Transform parent);
        
        public void RegisterCollect(ItemBehaviour item);
    }
}