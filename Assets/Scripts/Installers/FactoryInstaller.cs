using Core.Player;
using Core.Police;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class FactoryInstaller : MonoInstaller
	{
		[SerializeField]
		private PlayerBehaviour playerPrefab;
		
		[SerializeField]
		private PoliceBehaviour policePrefab;
		
		[SerializeField]
		private Transform policeParent;
		
		public override void InstallBindings()
		{
			Container
				.BindFactory<PlayerBehaviour, PlayerBehaviour.Factory>()
				.FromComponentInNewPrefab(playerPrefab);
			
			Container
				.BindMemoryPool<PoliceBehaviour, PoliceBehaviour.PolicePool>()
				.WithInitialSize(10)
				.FromComponentInNewPrefab(policePrefab)
				.UnderTransform(policeParent);
		}
	}
}