using Code.Game.Map.Base;
using Code.Game.Map.Controller;
using Code.Game.Map.Data;
using UnityEngine;
using Zenject;

public class InjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
//        Container.Bind<IInitializable>().WhenInjectedInto<RegionController>();
//        Container.Bind<MapViewEvents>().AsSingle();
//        ViewInjectsInstaller.Install(Container);
    }
}