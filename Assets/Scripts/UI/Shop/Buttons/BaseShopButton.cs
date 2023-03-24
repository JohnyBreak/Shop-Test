using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShopButton : MonoBehaviour, IPurchaseable
{
    protected Button _button;
    protected ShopItemTypeTimePair _pair;

    public abstract void Purchase();

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public virtual void Init(ShopItem item, PurchaseButtonPricePair buttonPair, PurchaseManager purchaseManager, ShopItemTypeTimePair pair)
    {
    }

    public virtual void Init(ShopItemTypeTimePair pair)
    {
    }

    protected virtual void OnClick()
    {
    }

    protected virtual void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
