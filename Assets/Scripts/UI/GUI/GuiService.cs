using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UI.Config;
using UI.GUI.Layer;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace UI.GUI
{
    [Serializable]
    public class GuiService : IGuiService
    {
        [field: SerializeField]
        private GuiConfig config;
        
        private readonly Dictionary<ScreenType, GameObject> _screens = new();
        private readonly Dictionary<GuiLayer, Transform> _layers = new();
        private readonly Dictionary<ScreenType, Queue<ScreenViewBase> > _poolOfScreens = new();

        private readonly Dictionary<GuiLayer, ILayerHandler> _layerHandlers = new()
        {
            {GuiLayer.Overlay, new CommonLayerHandler()}
        };

        private readonly HashSet<ScreenType> _inLoad = new();
        private readonly HashSet<ScreenType> _hideRequest = new();

        private bool _isPreload = true;

        [Inject]
        private DiContainer _diContainer;
        
        [Inject]
        private async void Initialize()
        {
            foreach (var item in config.layerDates)
            {
                var layer = Object.Instantiate(item.ScreenLayer.gameObject);
                
                Object.DontDestroyOnLoad(layer);
                
                _layers.Add(item.Layer, layer.transform);
            }
            
            foreach (var item in config.ScreensAssets)
            {
                var operation = item.AssetReference.LoadAssetAsync<GameObject>();
                
                await operation;
                
                if(operation.Status != AsyncOperationStatus.Succeeded)
                    throw new ArgumentException($"Can't preload a screen {item.ScreenType}");
                
                _screens.Add(item.ScreenType, operation.Result);
            }

            _isPreload = false;
        }

        public async UniTask<T> ShowScreen<T>(
            ScreenType screenType,
            GuiLayer layer,
            Action<T> callback)
            where T : ScreenViewModel, new()
        {
            if(_isPreload)
                await UniTask.WaitWhile(() => _isPreload);

            _inLoad.Add(screenType);
            
            var parent = _layers[layer];
            var layerHandler = _layerHandlers[layer];
            
            var viewModel = _diContainer.Instantiate<T>();
            
            callback?.Invoke(viewModel);
            
            ScreenViewBase screen;
            
            if (_poolOfScreens.ContainsKey(screenType))
            {
                var queue = _poolOfScreens[screenType];

                screen = queue.Dequeue();
                
                if(!queue.Any())
                    _poolOfScreens.Remove(screenType);
            }
            else
            {
                screen = _diContainer
                    .InstantiatePrefabForComponent<ScreenViewBase>(_screens[screenType], parent);
            }
            
            if(!screen.TryGetComponent(out ScreenViewBase screenView))
                throw new ArgumentException($"Can't get view component on screen {screenType}");

            screenView.Initialize(viewModel);
            
            layerHandler.HandleShow(screenView);
            
            try
            {
                return viewModel;
            }
            finally
            {
                _inLoad.Remove(screenType);
                    
                if(_hideRequest.Contains(screenType))
                    HideScreens(screenType, layer);
            }
        }

        public async void HideScreens(ScreenType screenType, GuiLayer guiLayer) 
        {
            if(_isPreload)
                await UniTask.WaitWhile(() => _isPreload);
            
            var layerHandler = _layerHandlers[guiLayer];
            var screens = layerHandler.FindScreensToHide(screenType).ToArray();

            if(!screens.Any() && _inLoad.Contains(screenType))
                _hideRequest.Add(screenType);

            if (!_poolOfScreens.ContainsKey(screenType) && screens.Any())
                _poolOfScreens.Add(screenType, new());
            
            foreach (var item in screens)
            {
                HideScreen(item, layerHandler);
            }
        }

        public void HideAllScreens()
        {
            foreach (var layerHandler in _layerHandlers.Values)
            {
                var screens = layerHandler.GetAllScreens();

                foreach (var screen in screens)
                {
                    HideScreen(screen, layerHandler);
                }
            }
        }

        private void HideScreen(
            ScreenViewBase screen,
            ILayerHandler layerHandler)
        {
            layerHandler.HandleHide(screen);
            
            _poolOfScreens[screen.ScreenType].Enqueue(screen);
        }
    }
}