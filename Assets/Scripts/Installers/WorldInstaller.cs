using System.Collections.Generic;
using Core;
using Core.Escape;
using Core.Item;
using Core.Level;
using Core.Path;
using Core.Player;
using Core.Police;
using UI.Config;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
	public class WorldInstaller : MonoInstaller
	{
		[field: SerializeField]
		private PlayerBehaviour playerPrefab;
		
		[field: SerializeField]
		private PoliceBehaviour policePrefab;
		
		[field: SerializeField]
		private EscapeBehaviour escapePrefab;
		
		[field: SerializeField]
		private Transform policeParent;
		
		[field: SerializeField]
		private Transform escapeParent;

		[Inject]
		private ILevelManager _levelManager;
		
		public override void InstallBindings()
		{
			Container
				.Bind<IPlayerManager>()
				.To<PlayerManager>()
				.FromNew()
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<IItemManager>()
				.To<ItemManager>()
				.FromNew()
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<IPoliceManager>()
				.To<PoliceManager>()
				.FromNew()
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<IPathManager>()
				.To<PathManager>()
				.FromNew()
				.AsSingle()
				.NonLazy();
			
			Container
				.Bind<IEscapeManager>()
				.To<EscapeManager>()
				.FromNew()
				.AsSingle()
				.NonLazy();

			Container
				.BindFactory<PlayerBehaviour, PlayerBehaviour.Factory>()
				.FromComponentInNewPrefab(playerPrefab)
				.NonLazy();
			
			Container
				.BindFactory<EscapeBehaviour, EscapeBehaviour.EscapeFactory>()
				.FromComponentInNewPrefab(escapePrefab)
				.UnderTransform(escapeParent)
				.AsSingle()
				.NonLazy();
			
			Container
				.BindMemoryPool<PoliceBehaviour, PoliceBehaviour.PolicePool>()
				.WithInitialSize(10)
				.FromComponentInNewPrefab(policePrefab)
				.UnderTransform(policeParent)
				.NonLazy();
			
			Container.Inject(new ZenjectInstantiate());
			Container.Inject(_levelManager);
		}
	}   
}