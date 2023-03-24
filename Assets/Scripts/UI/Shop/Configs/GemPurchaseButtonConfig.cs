
using UnityEngine;
[CreateAssetMenu(fileName = "GemPurchaseButtonConfig", menuName = "Shop/PurchaseButton/GemPurchaseButtonConfig")]
public class GemPurchaseButtonConfig : BasePurchaseButtonConfig
{
    public override bool Purchase(PurchaseManager manager, int price, ShopItemTypeTimePair pair)
    {
        return manager.HardPurchase(price, pair);
    }
}
