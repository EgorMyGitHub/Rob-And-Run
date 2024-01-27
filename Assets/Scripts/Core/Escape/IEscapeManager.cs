using System;
using UnityEngine;

namespace Core.Escape
{
    public interface IEscapeManager
    {
        event Action OnEscape;
        
        void RegisterSpawnEscape(Transform transform);
        void RegisterEscape();
    }
}