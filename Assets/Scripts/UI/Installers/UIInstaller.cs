using UI.GUI;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [field: SerializeField]
        private GuiService guiService = new();
    
        public override void InstallBindings()
        {
            Container
                .Bind<IGuiService>()
                .To<GuiService>()
                .FromInstance(guiService)
                .AsSingle()
                .NonLazy();
            
            Container.Inject(guiService);
        }
    }
}
