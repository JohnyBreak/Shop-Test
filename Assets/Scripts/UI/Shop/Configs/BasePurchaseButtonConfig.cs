using UnityEngine;

public abstract class BasePurchaseButtonConfig : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _purchaseText = "Get for ";

    public Sprite Icon => _icon;
    public string PurchaseText => _purchaseText;

    public abstract bool Purchase(PurchaseManager manager, int price, ShopItemTypeTimePair pair);
}