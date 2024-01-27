using System;
using System.Collections.Generic;
using UI.GUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Config
{
    [CreateAssetMenu(menuName = "Configs/UI/GuiServiceConfig")]
    public class GuiConfig : ScriptableObject
    {
        [Serializable]
        internal struct ScreenAssetReference
        {
            [field: SerializeField]
            public ScreenType ScreenType { get; private set; }
            
            [field: SerializeField]
            public AssetReferenceGameObject AssetReference { get; private set; }

            internal ScreenAssetReference(
                ScreenType screenType,
                AssetReferenceGameObject assetReference)
            {
                ScreenType = screenType;
                AssetReference = assetReference;
            }
        }
        
        [field: SerializeField]
        private string screenFolderPath;
        
        [field: SerializeField]
        private string layersFolderPath;

        [field: SerializeField]
        internal List<ScreenAssetReference> ScreensAssets { get; private set; } = new();

        [field: SerializeField]
        internal List<LayerData> layerDates = new();
        
        private void OnValidate()
        {
            ScreensAssets.Clear();
            layerDates.Clear();

            var prefabGuids = AssetDatabase.FindAssets("t:prefab", new[] { screenFolderPath });

            foreach (var guid in prefabGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab.TryGetComponent<ScreenViewBase>(out var screenView))
                {
                    ScreensAssets.Add(
                        new ScreenAssetReference(
                            screenView.ScreenType,
                            new AssetReferenceGameObject(guid)));
                }
            }
            
            prefabGuids = AssetDatabase.FindAssets("t:prefab", new[] { layersFolderPath });

            foreach (var guid in prefabGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab.TryGetComponent<ScreenLayer>(out var screenLayer))
                {
                    layerDates.Add(new LayerData(screenLayer.layer, screenLayer));
                }
            }
        }
    }
}