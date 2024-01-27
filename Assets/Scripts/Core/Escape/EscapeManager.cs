using System;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.Escape
{
    public class EscapeManager : IEscapeManager
    {
        [Inject]
        private EscapeBehaviour.EscapeFactory _escapePool;

        public event Action OnEscape;

        public void RegisterSpawnEscape(Transform transform)
        {
            var escape = ZenjectInstantiate.SpawnFromFactory(_escapePool, new SpawnInfo(transform.position, transform));
        }

        public void RegisterEscape()
        {
            OnEscape?.Invoke();
        }
    }
}