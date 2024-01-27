using Core.Escape;
using Core.Item;
using Core.Path;
using Core.Police;
using UnityEngine;
using Zenject;

namespace Core.Level
{
	public class Level : MonoBehaviour
	{
		[field: SerializeField]
		private PatrolPath[] pathPoints;
		
		[field: SerializeField]
		private Transform[] itemSpawnPosition;
		
		[field: SerializeField]
		private Transform[] escapeSpawnPosition;
		
		[field: SerializeField]
		private ItemsConfig itemsConfig;
		
		[field: SerializeField]
		private Transform itemTransform;
		
		[Inject]
		private IEscapeManager _escapeManager;
		
		[Inject]
		private IPoliceManager _policeManager;
		
		[Inject]
		private IItemManager _itemManager;
		
		public void Load()
		{
			foreach (var path in pathPoints)
			{
				_policeManager.SpawnPolice(path);
			}

			foreach (var item in itemSpawnPosition)
			{
				var itemTransform = item.transform;
				
				_itemManager.RegisterSpawn(
					itemTransform.position,
					itemTransform.rotation,
					GetRandomItem(),
					itemTransform);
			}

			foreach (var item in escapeSpawnPosition)
			{
				_escapeManager.RegisterSpawnEscape(item);
			}
		}

		private ItemBehaviour GetRandomItem() =>
				itemsConfig.ItemPrefabs[Random.Range(0, itemsConfig.ItemPrefabs.Count)];
	}
}