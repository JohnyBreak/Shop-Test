using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] private ShopItem _shopItemPrefab;
    [SerializeField] private BaseShopButton _purchaseButtonPrefab;
    [SerializeField] private BaseShopButton _useButtonPrefab;
    [SerializeField] private Transform _itemsHolder;
    private SaveManager _save;

    [Inject]
    private void Construct(SaveManager save)
    {
        _save = save;
    }

    public void Init(List<ShopItemConfig> configs, PurchaseManager purchaseManager)
    {
        //_save.Load();
        var pair = _save.SaveData.ShopItemTypeTimePair;

        foreach (var config in configs)
        {
            ShopItem item = Instantiate(_shopItemPrefab, _itemsHolder);

            ShopItemTypeTimePair tempPair = new ShopItemTypeTimePair();

            tempPair.ItemType = config.ItemType;
            bool saved = false;
            
            foreach (var type in pair)
            {
                if (type.ItemType == config.ItemType)
                {
                    saved = true;
                    tempPair.ItemType = type.ItemType;
                    tempPair.Time = type.Time;
                    break;
                }
            }

            if (saved)
            {
                item.SetInfo(this, config, purchaseManager, _useButtonPrefab, tempPair);

            }
            else
            {
                item.SetInfo(this, config, purchaseManager, _purchaseButtonPrefab, tempPair);
            }
        }
    }

    public void UpdateItemOnPurchase(ShopItem item) 
    {
        item.SwapToButton(_useButtonPrefab);
    }

    public void UpdateItemOnTimeOut(ShopItem item)
    {
        item.SwapToButton(_purchaseButtonPrefab);
    }

    public void SaveData() 
    {
        _save.Save();
    }

}
