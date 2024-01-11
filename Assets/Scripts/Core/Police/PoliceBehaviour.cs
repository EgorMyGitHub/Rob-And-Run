using Core.Point;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Core.Police
{
    public class PoliceBehaviour : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private NavMeshAgent navMeshAgent;
        
        [SerializeField, HideInInspector]
        private RendererViewZone viewZone;

        private QueuePoint _queuePoint;
        
        private Point.Point _targetPoint;

        private void OnValidate()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            viewZone = GetComponentInChildren<RendererViewZone>();
        }

        private void FixedUpdate()
        {
            Movement();
            viewZone.UpdateViewZone();
        }

        private void Movement()
        {
            if(Vector3.Distance(transform.position, _targetPoint.transform.position) < 0.1f)
                _targetPoint = _queuePoint.Next();
            
            navMeshAgent.destination = _targetPoint.transform.position;
        }

        public void InitializePoints(QueuePoint queuePoint)
        {
            _queuePoint = queuePoint;
            _targetPoint = _queuePoint.Next();
        }

        public class PolicePool : MonoMemoryPool<PoliceBehaviour>
        { }
    }
}
