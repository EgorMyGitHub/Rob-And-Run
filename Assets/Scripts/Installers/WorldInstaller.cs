using Core.Level;
using Core.Player;
using Zenject;

namespace Installers
{
	public class WorldInstaller : MonoInstaller
	{
		[Inject]
		private ILevelManager _levelManager;
		
		public override void InstallBindings()
		{
			Container
				.Bind<IPlayerManager>()
				.To<PlayerManager>()
				.FromNew()
				.AsSingle();
			
			Container.Inject(_levelManager);
		}
	}   
}