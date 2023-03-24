
using UnityEngine;
using Zenject;

public class ShopCanvasInstaller : MonoInstaller
{
    [SerializeField] private ShopCanvas _data;

    public override void InstallBindings()
    {
        Container.Bind<ShopCanvas>().FromInstance(_data).AsSingle().NonLazy();
    }
}
