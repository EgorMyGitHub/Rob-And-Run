using UniRx;

namespace Core.Player
{
	public interface IPlayerManager
	{
		IReadOnlyReactiveProperty<IPlayerBehaviour> Player{ get; }
		
		void DestroyPlayer();
	}
}