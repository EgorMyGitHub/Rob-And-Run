using Core.Player;
using UnityEngine;
using Zenject;

namespace Core.Item
{
    public class ItemBehaviour : MonoBehaviour
    {
        [Inject]
        private IItemManager _itemManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerBehaviour>(out var player))
            {
                _itemManager.RegisterCollect(this);
            }
        }
    }
}