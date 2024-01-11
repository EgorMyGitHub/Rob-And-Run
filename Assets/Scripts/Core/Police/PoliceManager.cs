using System.Collections.Generic;
using Core.Level;
using Core.Point;
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
        
        public void SpawnPolice(BasePoint point)
        {
            var newPolice = _policePool.Spawn();
            newPolice.InitializePoints(new QueuePoint(point));
            
            var policeTransform = newPolice.transform;
            var pointTransform = point.transform;
            
            policeTransform.position = pointTransform.position;
            policeTransform.rotation = pointTransform.rotation;
            
            _spawnedPolice.Add(newPolice);
        }

        private void DestroyAllPolice() =>
            _spawnedPolice.ForEach(i => Object.Destroy(i.gameObject));

        [Inject]
        private void Subscribe()
        {
            _levelManager.OnDestroyLevel += DestroyAllPolice;
        }
    }
}