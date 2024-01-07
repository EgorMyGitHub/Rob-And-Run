using Core.Level;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ILevelManager>()
                .To<LevelManager>()
                .FromNew()
                .AsSingle();
        }
    }
}