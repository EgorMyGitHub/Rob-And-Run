using System;
using Cysharp.Threading.Tasks;

namespace Core.Level
{
    public interface ILevelManager
    {
        event Action Loaded;
        event Action OnDestroyLevel;
        
        UniTask LoadLevelAsync(Level level, bool isTest);
        void DestroyLevel();
    }
}