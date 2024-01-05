namespace Core.Player
{
	public interface IPlayerManager
	{
		IPlayer Player { get; }
		
		void SpawnPlayer();
		void DestroyPlayer();
	}
}