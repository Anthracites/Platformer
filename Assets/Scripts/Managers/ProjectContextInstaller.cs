using UnityEngine;
using Zenject;
using Platformer.UIConnection;
using Platformer.GamePlay;
using Platformer.UserInterface;

public class ProjectContextInstaller : MonoInstaller
{
    public GameObject Pref;
    public override void InstallBindings()
    {
        BindManagers();
        BindFactories();
    }

    void BindManagers()
    {
        Container.Bind<UI_Manager>().AsSingle().NonLazy();
        Container.Bind<GamePlay_Manager>().AsSingle().NonLazy();
        Container.Bind<PopUp_Manager>().AsSingle().NonLazy();
    }

    void BindFactories()
    {
        Container.BindFactory<string, CharacterConroller, CharacterConroller.Factory>().FromFactory<PrefabResourceFactory<CharacterConroller>>();
        Container.BindFactory<string, BorderCS, BorderCS.Factory>().FromFactory<PrefabResourceFactory<BorderCS>>();
    }

}