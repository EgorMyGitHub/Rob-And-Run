using System.Collections.Generic;
using Core.Level;
using Core.Path;
using Core.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.Police
{
    public class PoliceManager : IPoliceManager
    {
        [Inject] 
        private PoliceBehaviour.PolicePool _policePool;
        
        [Inject]
        private ILevelManager _levelManager;
        
        [Inject]
        private IPlayerManager _playerManager;
        
        [Inject]
        private IPathManager _pathManager;
        
        private readonly List<PoliceBehaviour> _registeredPolice = new();
        
        private bool _isUpdatePoliceRender;
        
        private Transform _playerTransform;
        
        private const float UpdateTime = 0.5f;

        public void SpawnPolice(PatrolPath path)
        {
            var pathTransform = path.transform;
            
            var newPolice = ZenjectInstantiate.SpawnFromPool(
                _policePool,
                new SpawnInfo(
                    pathTransform.position,
                    pathTransform.rotation,
                    null));
            
            _registeredPolice.Add(newPolice);
            _pathManager.AddPolice(newPolice, path);
        }

        private async void DestroyAllPolice()
        {
            await UniTask.WaitWhile(() => _isUpdatePoliceRender);
            
            _registeredPolice.ForEach(i => Object.Destroy(i.gameObject));
        }

        private async UniTask StartUpdatePoliceRender()
        {
            while (true)
            {
                _isUpdatePoliceRender = true;

                foreach (var item in _registeredPolice)
                {
                    item.IsRenderZone = Vector3.Distance(
                        item.transform.position,
                        _playerTransform.position) < 16f;
                }
                
                _isUpdatePoliceRender = false;
                
                await UniTask.WaitForSeconds(UpdateTime);
            }
        }
        
        [Inject]
        private void Subscribe()
        {
            _levelManager.OnDestroyLevel += DestroyAllPolice;
            
            _playerManager.PlayerSpawned += (player) =>
            {
                _playerTransform = player.GameObject.transform;
                StartUpdatePoliceRender();
            };
        }
    }
}