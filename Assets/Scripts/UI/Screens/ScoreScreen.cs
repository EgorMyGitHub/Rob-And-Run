using Core.Level;
using UI.GUI;
using UI.ViewModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class ScoreScreen : ScreenView<ScoreViewModel>
    {
        [field: SerializeField]
        private Slider scoreBar;
        
        [field: SerializeField]
        private Button nextLevel;
        
        [field: SerializeField]
        private Button returnToMenu;
        
        [Inject]
        private ILevelManager _levelManager;
        
        public override void Show()
        {
            nextLevel.onClick.AddListener(NextLevel);
            returnToMenu.onClick.AddListener(ReturnToMenu);
            
            scoreBar.value = (float)ViewModel.MaxScore.Value / ViewModel.CollectedScore.Value;
        }

        public override void Hide()
        {
            base.Hide();
            
            nextLevel.onClick.RemoveListener(NextLevel);
            returnToMenu.onClick.RemoveListener(ReturnToMenu);
        }

        private void NextLevel()
        {
            _levelManager.LoadNextLevel();
        }

        private void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
