using UnityEngine;
using Zenject;

public class PurchaseManagerInstaller : MonoInstaller
{
    [SerializeField] private PurchaseManager _data;

    public override void InstallBindings()
    {
        Container.Bind<PurchaseManager>().FromInstance(_data).AsSingle().NonLazy();
    }
}
