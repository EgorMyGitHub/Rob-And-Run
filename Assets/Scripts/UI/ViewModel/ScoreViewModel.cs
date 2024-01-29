using Core.Item;
using UI.GUI;
using UniRx;
using Zenject;

namespace UI.ViewModel
{
    public class ScoreViewModel : ScreenViewModel
    {
        public IReadOnlyReactiveProperty<int> MaxScore => _maxScore;
        public IReadOnlyReactiveProperty<int> CollectedScore => _collectedScore;
        
        private readonly ReactiveProperty<int> _maxScore = new();
        private readonly ReactiveProperty<int> _collectedScore = new();

        [Inject]
        private IItemManager _itemManager;
        
        public void SetData()
        {
            _itemManager.AllItems.Subscribe(i => _maxScore.Value = i);
            _itemManager.CollectedItems.Subscribe(i => _collectedScore.Value = i);
        }
    }
}