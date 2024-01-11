using Core.Level;
using Core.Player;
using Core.Police;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class WorldInstaller : MonoInstaller
	{
		[Inject]
		private ILevelManager _levelManager;
		
		[SerializeField]
		private PlayerBehaviour playerPrefab;
		
		[SerializeField]
		private PoliceBehaviour policePrefab;
		
		[SerializeField]
		private Transform policeParent;
		
		public override void InstallBindings()
		{
			Container
				.Bind<IPlayerManager>()
				.To<PlayerManager>()
				.FromNew()
				.AsSingle();
			
			Container
				.Bind<IPoliceManager>()
				.To<PoliceManager>()
				.FromNew()
				.AsSingle();

			Container
				.BindFactory<PlayerBehaviour, PlayerBehaviour.Factory>()
				.FromComponentInNewPrefab(playerPrefab);
			
			Container
				.BindMemoryPool<PoliceBehaviour, PoliceBehaviour.PolicePool>()
				.WithInitialSize(10)
				.FromComponentInNewPrefab(policePrefab)
				.UnderTransform(policeParent);
			
			Container.Inject(_levelManager);
		}
	}   
}