using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Core.Player
{
	public class PlayerManager : IPlayerManager
	{
		[Inject] private Player.Factory _factory;
		
		[CanBeNull] public IPlayer Player { get; private set; }
		
		public void SpawnPlayer()
		{
			Player = _factory.Create();
		}

		public void DestroyPlayer()
		{
			Object.Destroy(Player.GameObject);
			Player = null;
		}
	}
}
