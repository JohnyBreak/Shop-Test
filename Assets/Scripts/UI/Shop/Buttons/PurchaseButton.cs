using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Zenject;

[RequireComponent(typeof(Button))]
public class PurchaseButton : BaseShopButton
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    private ShopItem _shopItem;
    private PurchaseManager _purchaseManager;
    private BasePurchaseButtonConfig _config;
    private int _price;
    

    public override void Init(ShopItem item, PurchaseButtonPricePair buttonPair, PurchaseManager purchaseManager, ShopItemTypeTimePair pair)
    {
        _shopItem = item;
        _pair = pair;
        _purchaseManager = purchaseManager;
        _price = buttonPair.Price;
        _config = buttonPair.PurchaseButton;
        _text.text = $"{_config.PurchaseText} {_price}";
        _icon.sprite = _config.Icon;
    }

    protected override void OnClick()
    {
        Purchase();
    }

    public override void Purchase()
    {
        if (_config.Purchase(_purchaseManager, _price, _pair)) 
        {
            _shopItem.UpdateView();
        }
    }
}
