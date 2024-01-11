using Core.Player;
using UnityEngine;
using Zenject;

namespace Core.Camera
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField]
		private Vector3 offset;
		
		[SerializeField]
		private float speed;
		
		[Inject]
		private IPlayerManager _playerManager;

		private IPlayerBehaviour _playerBehaviour;

		private void FixedUpdate()
		{
			if(_playerManager.Player.Value == null)
				return;
			
			_playerBehaviour ??= _playerManager.Player.Value;
			
			var target = _playerBehaviour.GameObject.transform.position + offset;
			
			transform.position =
				Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
		}
	}	
}
