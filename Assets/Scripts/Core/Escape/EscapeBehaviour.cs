using Core.Player;
using UnityEngine;
using Zenject;

namespace Core.Escape
{
    public class EscapeBehaviour : MonoBehaviour
    {
        [Inject]
        private IEscapeManager _escapeManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerBehaviour>(out var player))
            {
                _escapeManager.RegisterEscape();
            }
        }

        public class EscapeFactory : PlaceholderFactory<EscapeBehaviour>
        { }
    }
}