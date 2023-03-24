using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class WalletCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _softCurrencyText;
    [SerializeField] private TextMeshProUGUI _hardCurrencyText;

    private Wallet _wallet;
    private int _softAmount = 0;
    private int _hardAmount = 0;
    private int _remains = 0;
    private Coroutine _changeSoftRoutine;
    private Coroutine _changeHardRoutine;

    private void Awake()
    {
        _wallet.AmountChangeEvent += OnAmountChange;
        _wallet.AmountSetEvent += OnAmountSet;

        //_softAmount = _wallet.SoftCurrencyAmount;
        //_hardAmount = _wallet.HardCurrencyAmount;
        //_softCurrencyText.text = _softAmount.ToString();
        //_hardCurrencyText.text = _hardAmount.ToString();
    }

    private void OnDestroy()
    {
        _wallet.AmountChangeEvent -= OnAmountChange;
        _wallet.AmountSetEvent -= OnAmountSet;
    }

    [Inject]
    private void Construct(Wallet wallet) 
    {
        _wallet = wallet;
    }

    private void OnAmountSet(Wallet.CurrencyType type, int amount)
    {

        switch (type)
        {
            case Wallet.CurrencyType.Soft:
                _softAmount = amount;
                _softCurrencyText.text = amount.ToString();
                break;
            case Wallet.CurrencyType.Hard:
                _hardAmount = amount;
                _hardCurrencyText.text = amount.ToString();
                break;
        }
    }

    private void OnAmountChange(Wallet.CurrencyType type, int amount) 
    {
        
        switch (type)
        {
            case Wallet.CurrencyType.Soft:
                if (_changeSoftRoutine != null)
                {
                    StopCoroutine(_changeSoftRoutine);
                    _changeSoftRoutine = null;
                }

                _changeSoftRoutine = StartCoroutine(ChangeNumberRoutine(amount, type, _softAmount));
                break;
            case Wallet.CurrencyType.Hard:
                if (_changeHardRoutine != null)
                {
                    StopCoroutine(_changeHardRoutine);
                    _changeHardRoutine = null;
                }

                _changeHardRoutine = StartCoroutine(ChangeNumberRoutine(amount, type, _hardAmount));
                break;
        }
    }

    private IEnumerator ChangeNumberRoutine(int addMoney, Wallet.CurrencyType type, int am)
    {
        int amount = am;
        int sign = System.Math.Sign(addMoney);
        _remains += addMoney;
        int end = _remains + amount;
        yield return null;
        while (Mathf.Abs(_remains) != 0)
        {
            amount += sign;
            _remains -= sign;

            switch (type)
            {
                case Wallet.CurrencyType.Soft:
                    _softCurrencyText.text = amount.ToString();
                    break;
                case Wallet.CurrencyType.Hard:
                    _hardCurrencyText.text = amount.ToString();
                    break;
            }

            yield return new WaitForSeconds(.015f);
        }
        switch (type)
        {
            case Wallet.CurrencyType.Soft:
                _softAmount = amount;
                break;
            case Wallet.CurrencyType.Hard:
                _hardAmount = amount;
                break;
        }
    }
}

