using Zenject;
using Platformer.UIConnection;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UI_Manager>().AsSingle().NonLazy();
    }
}