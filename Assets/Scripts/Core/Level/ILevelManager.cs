using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Core.Level
{
    public interface ILevelManager
    {
        event Action Loaded;
        event Action OnDestroyLevel;
        
        UniTask LoadLevelAsync(Level levelToLoad, IEnumerator<Level> allLevel, bool isTest);
        void LoadNextLevel();
        void DestroyLevel();
    }
}