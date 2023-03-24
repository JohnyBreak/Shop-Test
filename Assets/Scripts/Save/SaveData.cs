using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int HardCurrencyAmount;
    public int SoftCurrencyAmount;
    public List<ShopItemTypeTimePair> ShopItemTypeTimePair;
}

[System.Serializable]
public class ShopItemTypeTimePair
{
    public ItemType ItemType = ItemType.None;
    public string Time = "0:0:0";
}