using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class StateContainer<TEnum> 
        where TEnum: struct
    {
        private readonly Dictionary<GameObject, StateItem<TEnum>> _stateContainer = new();

        public StateItem<TEnum> AddState(GameObject forObject, TEnum startState, params Action<TEnum>[] callback)
        {
            var state = new StateItem<TEnum>(startState, callback);
            _stateContainer.Add(forObject, state);
            
            return state;
        }
        
        public void UpdateState(GameObject forObject, TEnum newState) =>
            _stateContainer[forObject].UpdateState(newState);

        public void AddCallback(GameObject forObject, Action<TEnum>[] callback) =>
            _stateContainer[forObject].Callback.AddRange(callback);

        public StateItem<TEnum> this[GameObject from] => _stateContainer[from];
    }
}
