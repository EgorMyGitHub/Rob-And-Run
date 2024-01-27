using Core.Player;
using UnityEngine;
using Zenject;

namespace Core.Camera
{
	public class CameraFollow : MonoBehaviour
	{
		[field: SerializeField]
		private Vector3 offset;
		
		[field: SerializeField]
		private float speed;
		
		[Inject]
		private IPlayerManager _playerManager;

		private IPlayerBehaviour _playerBehaviour;

		private void Awake()
		{
			_playerManager.PlayerSpawned += i => _playerBehaviour = i;
		}

		private void FixedUpdate()
		{
			if (_playerBehaviour == null)
				return;

			var target = _playerBehaviour.GameObject.transform.position + offset;
			
			transform.position =
				Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
		}
	}	
}
