using Core.Level;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TestLoad : MonoBehaviour
    {
        [SerializeField]
        private Level.Level test;
    
        [Inject]
        private ILevelManager _manager;

        private void Start()
        {
            _manager.LoadLevelAsync(test, true);
        }
    }
}
