using UnityEngine;
using System;
using Zenject;

public class Wallet : MonoBehaviour
{
    public enum CurrencyType 
    {
        Soft,
        Hard,
    }

    public Action<CurrencyType, int> AmountChangeEvent;

    public Action<CurrencyType, int> AmountSetEvent;

    public Action<CurrencyType, int> AmountRemoveEvent;

    private SaveManager _saveManager;
    private int _softCurrencyAmount;
    private int _hardCurrencyAmount;

    public int SoftCurrencyAmount => _softCurrencyAmount;
    public int HardCurrencyAmount => _hardCurrencyAmount;

    [Inject]
    private void Construct(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    private void Start()
    {
        _saveManager.Load(); 
        SetMoney(_saveManager.SaveData.SoftCurrencyAmount, CurrencyType.Soft);
        SetMoney(_saveManager.SaveData.HardCurrencyAmount, CurrencyType.Hard, true);
    }

    public void GiveMoney()
    {
        SetMoney(100, CurrencyType.Soft);
        SetMoney(100, CurrencyType.Hard, true);
    }

    public void SetMoney(int amount, CurrencyType type, bool save = false) 
    {
        switch (type)
        {
            case CurrencyType.Soft:
                _softCurrencyAmount = amount;
                break;
            case CurrencyType.Hard:
                _hardCurrencyAmount = amount;
                break;
        }

        if(save) SaveMoney();

        AmountSetEvent?.Invoke(type, amount);
    }

    public void AddMoney(int amount, CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.Soft:
                _softCurrencyAmount += amount;
                break;
            case CurrencyType.Hard:
                _hardCurrencyAmount += amount;
                break;
        }

        SaveMoney();
        AmountChangeEvent?.Invoke(type, amount);
    }

    public bool RemoveMoney(int amount, CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.Soft:

                if (amount > _softCurrencyAmount) return false;

                _softCurrencyAmount -= amount;
                break;
            case CurrencyType.Hard:

                if (amount > _hardCurrencyAmount) return false;

                _hardCurrencyAmount -= amount;
                break;
        }

        SaveMoney();
        AmountChangeEvent?.Invoke(type, -amount);
        return true;
    }

    //public bool CheckMoney(int amount, CurrencyType type)
    //{
    //    if (amount > _softCurrencyAmount) return false;

    //    return true;
    //}

    private void SaveMoney()
    {
        _saveManager.SaveData.SoftCurrencyAmount = _softCurrencyAmount;
        _saveManager.SaveData.HardCurrencyAmount = _hardCurrencyAmount;

        _saveManager.Save();
    }

}
