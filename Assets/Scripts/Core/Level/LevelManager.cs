using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Utils;
using Object = UnityEngine.Object;

namespace Core.Level
{
	public class LevelManager : ILevelManager
	{
		public event Action Loaded;
		public event Action OnDestroyLevel;
		
		private Level _currentLevel;

		public async UniTask LoadLevelAsync(Level level, bool isTest)
		{
			if(isTest)
			{
				await UniTask.WaitForFixedUpdate();
				
				Loaded?.Invoke();
				return;
			}
			
			var scene = SceneManager.LoadSceneAsync("TestLevel");

			await UniTask.WaitWhile(() => !scene.isDone);

			_currentLevel = ZenjectInstantiate.InstantiatePrefabForComponent(level);
			_currentLevel.Load();

			Loaded?.Invoke();
		}
		
		public void DestroyLevel()
		{
			OnDestroyLevel?.Invoke();
			
			Object.Destroy(_currentLevel.gameObject);
		}
	}
}
