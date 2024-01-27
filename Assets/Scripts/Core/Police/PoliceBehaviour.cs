using System;
using Core.Path;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Zenject;

namespace Core.Police
{
    public class PoliceBehaviour : MonoBehaviour
    {
        [field: SerializeField, HideInInspector]
        private Animator animator;
        
        [field: SerializeField, HideInInspector]
        private NavMeshAgent navMeshAgent;
        
        [field: SerializeField, HideInInspector]
        public RendererViewZone ViewZone{ get; private set; }
        
        private StateItem<PatrolState> _stateItem;
        
        [Inject]
        private IPathManager _pathManager;
        
        public Vector3 TargetPoint { get; set; }

        [field: NonSerialized]
        public bool IsRenderZone;

        public float Distance => navMeshAgent.remainingDistance;
        
        private void OnValidate()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            ViewZone = GetComponentInChildren<RendererViewZone>();
        }

        private void FixedUpdate()
        {
            if(IsRenderZone)
                ViewZone.UpdateViewZone();
            
            _pathManager.UpdatePath(this);
                
            SetDestination();
        }

        private async void OnUpdateState(PatrolState newState)
        {
            if(newState == PatrolState.LookAround)
                LookAround();
        }

        private void SetDestination() =>
            navMeshAgent.destination = TargetPoint;
        
        public async UniTask LookAround()
        {
            animator.SetBool("LookAround", true);
            
            _stateItem.CancelState.Token.Register(() =>
            {
                var rotate = transform.rotation;
                animator.SetBool("LookAround", false);
                SetDestination();
                transform.rotation = rotate;
            });
            
            await UniTask.WaitForSeconds(1.5f, cancellationToken: _stateItem.CancelState.Token);
            
            if(_stateItem.State == PatrolState.LookAround)
                _stateItem.UpdateState(PatrolState.Patrol);
            
            animator.SetBool("LookAround", false);
        }

        public void Initialize(StateItem<PatrolState> state)
        {
            _stateItem = state;
            _stateItem.Callback.Add(OnUpdateState);
        }
        
        public class PolicePool : MonoMemoryPool<PoliceBehaviour>
        { }
    }
}
