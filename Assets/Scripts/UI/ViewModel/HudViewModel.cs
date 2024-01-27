using Core.Item;
using UI.GUI;
using UniRx;

namespace UI.ViewModel
{
    public class HudViewModel : ScreenViewModel
    {
        public IReadOnlyReactiveProperty<int> CollectItemCount => _collectItemCount;
        public IReadOnlyReactiveProperty<int> AllItemCount => _allItemCount;
        
        private readonly ReactiveProperty<int> _collectItemCount = new();
        private readonly ReactiveProperty<int> _allItemCount = new();

        public void SetData(IItemManager itemManager)
        {
            itemManager.CollectedItems.Subscribe(i => _collectItemCount.Value = i).AddTo(DisposableList);
            itemManager.AllItems.Subscribe(i => _allItemCount.Value = i).AddTo(DisposableList);
        }
    }
}
