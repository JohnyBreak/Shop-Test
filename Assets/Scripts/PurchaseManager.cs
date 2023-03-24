using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PurchaseManager : MonoBehaviour
{
    private Wallet _wallet;
    private SaveManager _save;

    [Inject]
    private void Construct(Wallet wallet, SaveManager save)
    {
        _wallet = wallet;
        _save = save;
    }

    public bool SoftPurchase(int amount, ShopItemTypeTimePair pair)
    {
        if (_wallet.RemoveMoney(amount, Wallet.CurrencyType.Soft) == false) return false;

        _save.SaveData.ShopItemTypeTimePair.Add(pair);
        Debug.LogError($"soft {amount} {pair.ItemType}");
        _save.Save();
        return true;
    }

    public bool HardPurchase(int amount, ShopItemTypeTimePair pair)
    {
        if (_wallet.RemoveMoney(amount, Wallet.CurrencyType.Hard) == false) return false;

        _save.SaveData.ShopItemTypeTimePair.Add(pair);
        Debug.LogError($"hard {amount} {pair.ItemType}");
        _save.Save();
        return true;
    }

    //public void RealMoneyPurchase(int amount)
    //{
    //    Debug.LogError($"buy for real money");
    //}
}
