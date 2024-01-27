using System;
using Cysharp.Threading.Tasks;

namespace UI.GUI
{
    public enum ScreenType
    {
        Hud
    }

    public enum GuiLayer
    {
        Overlay
    }
    
    public interface IGuiService
    {
        public UniTask<T> ShowScreen<T>(
            ScreenType screenType,
            GuiLayer layer,
            Action<T> callback)
            where T: ScreenViewModel, new();
        
        public void HideScreens(ScreenType screenType, GuiLayer guiLayer);
        
        public void HideAllScreens();
    }
}