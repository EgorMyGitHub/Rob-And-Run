using System.Collections.Generic;

namespace UI.GUI.Layer
{
    public class CommonLayerHandler : ILayerHandler
    {
        private readonly List<ScreenViewBase> _screens = new();
        
        public IEnumerable<ScreenViewBase> FindScreensToHide(ScreenType screenType)
        {
            return _screens.FindAll(i => i.ScreenType == screenType);
        }

        public IEnumerable<ScreenViewBase> GetAllScreens() => _screens;

        public void HandleShow(ScreenViewBase screenViewBase)
        {
            _screens.Add(screenViewBase);
            screenViewBase.Show();
        }

        public void HandleHide(ScreenViewBase screenViewBase)
        {
            _screens.Remove(screenViewBase);
            screenViewBase.Hide();
        }
    }
}