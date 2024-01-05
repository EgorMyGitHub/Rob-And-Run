using Core.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class FactoryInstaller : MonoInstaller
	{
		[SerializeField] private Player playerPrefab;
		
		public override void InstallBindings()
		{
			Container
				.BindFactory<Player, Player.Factory>()
				.FromComponentInNewPrefab(playerPrefab);
		}
	}
}