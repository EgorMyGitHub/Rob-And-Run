using TMPro;
using UI.GUI;
using UI.ViewModel;
using UniRx;
using UnityEngine;

namespace UI.Screens
{
    public class HudScreen : ScreenView<HudViewModel>
    {
        [field: SerializeField]
        private TMP_Text collectedItem;

        public override void Show()
        {
            ViewModel.CollectItemCount.Subscribe(_ => UpdateText()).AddTo(DisposableList);
            ViewModel.AllItemCount.Subscribe(_ => UpdateText()).AddTo(DisposableList);
        }

        private void UpdateText()
        {
            Debug.Log("Update");
            collectedItem.text = $"{ViewModel.CollectItemCount.Value}/{ViewModel.AllItemCount.Value}";
        }
    }
}
