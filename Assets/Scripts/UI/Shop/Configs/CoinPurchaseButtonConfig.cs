using UnityEngine;

[CreateAssetMenu(fileName = "CoinPurchaseButtonConfig", menuName = "Shop/PurchaseButton/CoinPurchaseButtonConfig")]
public class CoinPurchaseButtonConfig : BasePurchaseButtonConfig
{
    public override bool Purchase(PurchaseManager manager, int price, ShopItemTypeTimePair pair)
    {
       return manager.SoftPurchase(price, pair);
    }
}
