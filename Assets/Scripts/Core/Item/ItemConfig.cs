using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Item
{
    [CreateAssetMenu(menuName = "Configs/Items")]
    public class ItemsConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<ItemBehaviour> ItemPrefabs{ get; private set; }
        
        [field: SerializeField]
        private string itemFolderPath;

        private void OnValidate()
        {
            ItemPrefabs.Clear();

            var prefabGuids = AssetDatabase.FindAssets("t:prefab", new[] { itemFolderPath });

            foreach (var guid in prefabGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab.TryGetComponent<ItemBehaviour>(out var item))
                {
                    ItemPrefabs.Add(item);
                }
            }
        }
    }
}
