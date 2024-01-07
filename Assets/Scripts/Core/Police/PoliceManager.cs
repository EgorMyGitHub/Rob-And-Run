using System.Collections.Generic;
using Core.Level;
using UnityEngine;
using Zenject;

namespace Core.Police
{
    public class PoliceManager : IPoliceManager
    {
        [Inject] 
        private PoliceBehaviour.PolicePool _policePool;
        
        [Inject]
        private ILevelManager _levelManager;
        
        private List<PoliceBehaviour> _spawnedPolice = new();
        
        public void SpawnPolice(Point point)
        {
            var newPolice = _policePool.Spawn();
            
            var newPoliceTransform = newPolice.transform;
            
            newPoliceTransform.position = point.transform.position;
            newPoliceTransform.rotation = point.transform.rotation;
            
            _spawnedPolice.Add(newPolice);
        }

        private void DestroyAllPolice() =>
            _spawnedPolice.ForEach(i => Object.Destroy(i.gameObject));

        [Inject]
        private void Subscribe()
        {
            _levelManager.onDestroyLevel += DestroyAllPolice;
        }
    }
}