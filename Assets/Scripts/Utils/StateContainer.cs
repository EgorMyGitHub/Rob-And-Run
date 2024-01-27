using System;
using System.Collections.Generic;
using System.Threading;

namespace Utils
{
    public class StateItem<TEnum>
        where TEnum: struct
    {
        public StateItem(TEnum startState, params Action<TEnum>[] callbacks)
        {
            Callback = new(callbacks);
            State = startState;
        }

        public readonly List<Action<TEnum>> Callback;
        public TEnum State { get; private set; }

        public CancellationTokenSource CancelState = new();
        
        public void UpdateState(TEnum newState)
        {
            State = newState;

            CancelState.Cancel();
            CancelState = new();
            
            Callback.ForEach(i => i.Invoke(newState));
        }
    }
}