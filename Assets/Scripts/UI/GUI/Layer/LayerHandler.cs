using System.Collections.Generic;

namespace UI.GUI.Layer
{
    public interface ILayerHandler
    {
        public IEnumerable<ScreenViewBase> FindScreensToHide(ScreenType screenType);
        public IEnumerable<ScreenViewBase> GetAllScreens();

        public void HandleShow(ScreenViewBase screenViewBase);
        public void HandleHide(ScreenViewBase screenViewBase);
    }
}