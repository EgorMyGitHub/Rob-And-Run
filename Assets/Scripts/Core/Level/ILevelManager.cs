using System;
using Cysharp.Threading.Tasks;

namespace Core.Level
{
    public interface ILevelManager
    {
        event Action loaded;
        event Action onDestroyLevel;
        
        UniTask LoadLevelAsync(Level level, bool isTest);
        void DestroyLevel();
    }
}