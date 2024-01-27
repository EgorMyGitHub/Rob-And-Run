using System.Collections.Generic;
using System.Threading;
using Core.Player;
using Core.Point;
using Core.Police;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.Path
{
    public class PathManager : IPathManager
    {
        private readonly Dictionary<PoliceBehaviour, QueuePath> _policePath = new();
        private readonly Dictionary<PoliceBehaviour, (Transform, CancellationTokenSource)> _policeInFollow = new();
        private readonly StateContainer<PatrolState> _patrolStates = new();
        
        [Inject]
        private IPlayerManager _playerManager;
        
        public void AddPolice(PoliceBehaviour police, PatrolPath path)
        {
            _policePath.Add(police, new QueuePath(path));
            var state = _patrolStates.AddState(police.gameObject, PatrolState.Patrol);
            
            UpdatePath(police);
            
            police.Initialize(state);
            police.ViewZone.PlayerInView += (player, cancel) => StartFollow(police, player, cancel);
        }

        public void UpdatePath(PoliceBehaviour police)
        {
            switch (_patrolStates[police.gameObject].State)
            {
                case PatrolState.Patrol:
                    UpdateDefaultPath(police);
                    break;
                
                case PatrolState.Following:
                    UpdateFollowPath(police);
                    break;
            }
        }

        private void UpdateFollowPath(PoliceBehaviour police)
        {
            var follow = _policeInFollow[police];
            
            if (!follow.Item2.IsCancellationRequested)
            {
                police.TargetPoint = follow.Item1.position;
            }

            if (!(police.Distance < 1f)) 
                return;
            
            if (!follow.Item2.IsCancellationRequested)
                _playerManager.DestroyPlayer();
                
            _patrolStates.UpdateState(police.gameObject, PatrolState.LookAround);
            _policeInFollow.Remove(police);
        }
        
        private void UpdateDefaultPath(PoliceBehaviour police)
        {
            if (police.TargetPoint == Vector3.zero 
            ||  police.Distance < 0.1f)
                police.TargetPoint = _policePath[police].Next();
        }
        
        private void StartFollow(
            PoliceBehaviour police,
            PlayerBehaviour player,
            CancellationTokenSource cancel)
        {
            if(!_policeInFollow.ContainsKey(police))
                _policeInFollow.Add(police, default);
            
            _policeInFollow[police] = (player.transform, cancel);

            police.TargetPoint = _policeInFollow[police].Item1.position;
            
            _patrolStates.UpdateState(police.gameObject, PatrolState.Following);
        }
    }
}