using System;
using UniRx;

namespace Core.Player
{
	public interface IPlayerManager
	{
		IReadOnlyReactiveProperty<IPlayerBehaviour> PlayerInstance{ get; }
		
		event Action<IPlayerBehaviour> PlayerSpawned;
		event Action PlayerDestroy;
		
		void DestroyPlayer();
	}
}