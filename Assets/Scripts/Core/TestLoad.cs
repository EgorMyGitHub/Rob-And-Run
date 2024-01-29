using System.Collections.Generic;
using Core.Level;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TestLoad : MonoBehaviour
    {
        [field: SerializeField]
        private Level.Level test;
        
        [field: SerializeField]
        private List<Level.Level> allLevels;
    
        [Inject]
        private ILevelManager _manager;

        private void Start()
        {
            var index = allLevels.IndexOf(test);
            var nextLevels = allLevels.GetRange(index, allLevels.Count - index).GetEnumerator();
            
            _manager.LoadLevelAsync(test, nextLevels, false);
        }
    }
}
