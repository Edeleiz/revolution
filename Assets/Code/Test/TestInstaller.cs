using Zenject;
using UnityEngine;
using System.Collections;

public class TestInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<MapViewEvents>().AsSingle();
        Container.Bind<Greeter>().AsSingle().NonLazy();
    }
}

public class Greeter
{
    [Inject] string mapEvents;
    
    public Greeter(string message)
    {
        Debug.Log(message);
    }

    public void ShowInjection()
    {
        Debug.Log(mapEvents);
    }
}

public class SomeShit
{
    public SomeShit()
    {
        var g = new Greeter("hey");
        g.ShowInjection();
    }
}