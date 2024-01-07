using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.Level
{
	public class LevelManager : ILevelManager
	{
		public event Action loaded;
		public event Action onDestroyLevel;
		
		[Inject] 
		private DiContainer _diContainer;
		
		private Level currentLevel;

		public async UniTask LoadLevelAsync(Level level, bool isTest)
		{
			if(isTest)
			{
				await UniTask.WaitForFixedUpdate();
				
				loaded?.Invoke();
				return;
			}
			
			var scene = SceneManager.LoadSceneAsync("TestLevel");

			await UniTask.WaitWhile(() => !scene.isDone);

			currentLevel = _diContainer.InstantiatePrefabForComponent<Level>(level);
			currentLevel.Load();

			loaded?.Invoke();
		}
		
		public void DestroyLevel()
		{
			onDestroyLevel?.Invoke();
			
			Object.Destroy(currentLevel.gameObject);
		}
	}
}
