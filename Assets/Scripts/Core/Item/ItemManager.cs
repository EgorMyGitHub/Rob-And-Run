using System.Collections.Generic;
using UI.GUI;
using UI.ViewModel;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.Item
{
    public class ItemManager : IItemManager
    {
        [Inject]
        private IGuiService _guiService;
        
        private List<ItemBehaviour> _items = new();

        private readonly ReactiveProperty<int> _collectedItems = new();
        private readonly ReactiveProperty<int> _allItems = new();

        public IReadOnlyReactiveProperty<int> CollectedItems => _collectedItems;
        public IReadOnlyReactiveProperty<int> AllItems => _allItems;

        public void RegisterSpawn(
            Vector3 position,
            Quaternion quaternion,
            ItemBehaviour item,
            Transform parent)
        {
            var newPrefab = ZenjectInstantiate.InstantiatePrefabForComponent(
                item, 
                new SpawnInfo(position, quaternion, parent));

            _items.Add(newPrefab);
            _allItems.Value++;
        }

        public void RegisterCollect(ItemBehaviour item)
        {
            _collectedItems.Value++;
            
            _items.Remove(item);
            Object.Destroy(item.gameObject);
        }

        [Inject]
        private async void Initialize()
        {
            await _guiService.ShowScreen<HudViewModel>(
                ScreenType.Hud,
                GuiLayer.Overlay,
                i => i.SetData(this));
        }
    }
}