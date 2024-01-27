using System;
using UnityEngine;

namespace UI.GUI
{
    [Serializable]
    public struct LayerData
    {
        public LayerData(GuiLayer layer, ScreenLayer screenLayer)
        {
            Layer = layer;
            ScreenLayer = screenLayer;
        }
        
        [field: SerializeField]
        public GuiLayer Layer { get; private set; }
            
        [field: SerializeField]
        public ScreenLayer ScreenLayer { get; private set; }
    }
}