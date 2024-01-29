using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
		private IEnumerator<Level> _nextLevels;

		public async UniTask LoadLevelAsync(Level levelToLoad, IEnumerator<Level> nextLevels, bool isTest)
		{
			_nextLevels = nextLevels;
			
			if(isTest)
			{
				await UniTask.WaitForFixedUpdate();
				
				Loaded?.Invoke();
				return;
			}

			var scene = SceneManager.LoadSceneAsync("TestLevel");

			await UniTask.WaitWhile(() => !scene.isDone);

			_currentLevel = ZenjectInstantiate.InstantiatePrefabForComponent(levelToLoad);
			_currentLevel.Load();

			Loaded?.Invoke();
		}

		public void LoadNextLevel()
		{
			DestroyLevel();

			if (!_nextLevels.MoveNext())
				Debug.LogError("All Levels Complete!");
			
			var levelToLoad = _nextLevels.Current;
			
			_currentLevel = ZenjectInstantiate.InstantiatePrefabForComponent(levelToLoad);
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
