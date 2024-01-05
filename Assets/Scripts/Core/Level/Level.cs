using Core.Player;
using UnityEngine;
using Zenject;

namespace Core.Level
{
	public class Level : MonoBehaviour
	{
		[Inject] private IPlayerManager _playerManager;
		
		public void Load()
		{
			_playerManager.SpawnPlayer();
		}
	}
}