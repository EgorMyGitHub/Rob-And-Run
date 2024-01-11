using Core.Level;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Player
{
	public class PlayerManager : IPlayerManager
	{
		[Inject]
		private PlayerBehaviour.Factory _factory;
		
		[Inject]
		private ILevelManager _levelManager;

		private readonly ReactiveProperty<IPlayerBehaviour> _player = new();
		
		public IReadOnlyReactiveProperty<IPlayerBehaviour> Player => _player;

		public void SpawnPlayer()
		{
			_player.Value = _factory.Create();
		}

		public void DestroyPlayer()
		{
			Object.Destroy(_player.Value.GameObject);
			_player.Value = null;
		}

		[Inject]
		private void Subscribe()
		{
			_levelManager.Loaded += SpawnPlayer;
			_levelManager.OnDestroyLevel += DestroyPlayer;
		}
	}
}
