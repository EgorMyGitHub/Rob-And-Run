using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.Level
{
	public class LevelManager : ILevelManager
	{
		public event Action Loaded;
		public event Action OnDestroyLevel;
		
		[Inject] 
		private DiContainer _diContainer;
		
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

			_currentLevel = _diContainer.InstantiatePrefabForComponent<Level>(level);
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
