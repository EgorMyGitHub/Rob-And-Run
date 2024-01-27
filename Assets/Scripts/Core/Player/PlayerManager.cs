using System;
using Core.Level;
using UniRx;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.Player
{
	public class PlayerManager : IPlayerManager
	{
		[Inject]
		private PlayerBehaviour.Factory _factory;
		
		[Inject]
		private ILevelManager _levelManager;

		private readonly ReactiveProperty<IPlayerBehaviour> _playerInstance = new();
		
		public IReadOnlyReactiveProperty<IPlayerBehaviour> PlayerInstance => _playerInstance;
		
		public event Action<IPlayerBehaviour> PlayerSpawned;
		public event Action PlayerDestroy;

		public void SpawnPlayer()
		{
			_playerInstance.Value = ZenjectInstantiate.SpawnFromFactory(_factory);
			PlayerSpawned?.Invoke(_playerInstance.Value);
		}

		public void DestroyPlayer()
		{
			Object.Destroy(_playerInstance.Value.GameObject);
			_playerInstance.Value = null;
			
			PlayerDestroy?.Invoke();
		}

		[Inject]
		private void Subscribe()
		{
			_levelManager.Loaded += SpawnPlayer;
			_levelManager.OnDestroyLevel += DestroyPlayer;
		}
	}
}
