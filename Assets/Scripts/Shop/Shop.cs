using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItemConfig> _configs;

    private ShopCanvas _shopCanvas;
    private PurchaseManager _purchaseManager;

    [Inject]
    private void Construct(ShopCanvas shopCanvas, PurchaseManager purchaseManager)
    {
        _shopCanvas = shopCanvas;
        _purchaseManager = purchaseManager;
    }

    private IEnumerator Start()
    {
        yield return null;

        _shopCanvas.Init(_configs, _purchaseManager);
    }
}