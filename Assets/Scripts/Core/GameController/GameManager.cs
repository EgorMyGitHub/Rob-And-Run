using Core.Escape;
using Core.Player;
using UI.GUI;
using Zenject;

namespace Core.GameController
{
    public class GameManager
    {
        [Inject]
        private IPlayerManager _playerManager;
        
        [Inject]
        private IEscapeManager _escapeManager;
        
        [Inject]
        private IGuiService _guiService;
        
        [Inject]
        private void Initialize()
        {
            _playerManager.PlayerDestroy += Caught;
            _escapeManager.OnEscape += Escape;
        }

        private void Caught()
        {
            GameOver();
        }

        private void Escape()
        {
            GameOver();
        }
        
        private void GameOver()
        {
            
        }
    }
}