using UnityEngine;
using UnityEngine.UI;

public class UseButton : BaseShopButton
{
    protected override void OnClick()
    {
        Debug.Log($"UseButton OnClick type: {_pair.ItemType}");
    }

    public override void Init(ShopItemTypeTimePair pair)
    {
        _pair = pair;
    }

    public override void Purchase()
    {
    }
}
