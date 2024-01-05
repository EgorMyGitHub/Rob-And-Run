using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Data;
using Zenject;

namespace Core.Level
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private List<Level> levels = new();

		[Inject] private DiContainer _diContainer;
		
		private void Awake()
		{
			var data = DataSave.Load<LevelInfo>();
			
			LoadLevel(data.Level - 1);
		}

		public void LoadLevel(int index)
		{
			var newLevel = _diContainer.InstantiatePrefabForComponent<Level>(levels[index]);
			
			newLevel.Load();
		}
	}   
}
