using Core.Police;
using UnityEngine;
using Zenject;

namespace Core.Level
{
	public class Level : MonoBehaviour
	{
		[SerializeField] private Point[] spawnPoints;
		
		[Inject]
		private IPoliceManager _policeManager;
		
		public void Load()
		{
			foreach (var item in spawnPoints)
			{
				_policeManager.SpawnPolice(item);
			}
		}
	}
}