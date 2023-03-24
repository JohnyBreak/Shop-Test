using UnityEngine.UI;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ShopItemTimer))]

public class ShopItem : MonoBehaviour
{

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Transform _buttonsHolder;
    [SerializeField] private GameObject _timerHolder;
    [SerializeField] private TextMeshProUGUI _timerText;

    private PurchaseManager _purchaseManager;
    private ShopItemTypeTimePair _pair;
    private ShopItemTimer _timer;
    private ShopCanvas _shopCanvas;
    private ShopItemConfig _config;

    public void SetInfo(ShopCanvas shopCanvas, ShopItemConfig config, PurchaseManager purchaseManager, BaseShopButton buttonPrefab, ShopItemTypeTimePair pair) 
    {
        _config = config;
        _timer = GetComponent<ShopItemTimer>();
        _purchaseManager = purchaseManager;
        _shopCanvas = shopCanvas;
        _timerHolder.SetActive(false);
        _pair = pair;
        _icon.sprite = config.Icon;
        _nameText.text = config.Name;

        if (buttonPrefab is UseButton)
        {
            var button = Instantiate((UseButton)buttonPrefab, _buttonsHolder);
            button.Init(_pair);

            if (HasTimer()) 
            {
                SetTimer();
            }

            return;
        }

        foreach (var item in config.PurchaseButtonPricePairs)
        {
            var button = Instantiate((PurchaseButton)buttonPrefab, _buttonsHolder);
            button.Init(this, item, purchaseManager, _pair);
        }
    }

    public void UpdateView() 
    {
        _shopCanvas.UpdateItemOnPurchase(this);
    }

    public void SetTimer() 
    {
        if (_timer.NeedToSetTimer(_pair.Time) == false)
        {
            _timer.TimeOut -= OnTimeOut;
            OnTimeOut();
            return;
        }

        _timerHolder.SetActive(true);
        _timer.StartCountDown(_timerText, _pair.Time);
        
        _timer.TimeOut += OnTimeOut;
    }

    

    private bool HasTimer()
    {
        string time = $"{_config.Hour}:{_config.Min}:{_config.Sec}";

        if (time != "0:0:0") return true;

        return false;
    }

    //public void SwapToUseButton(BaseShopButton buttonPrefab) 
    //{
    //    int childs = _buttonsHolder.childCount;

    //    for (int i = childs - 1; i > -1; i--)
    //    {
    //        Destroy(_buttonsHolder.GetChild(i).gameObject);
    //    }
    //    var button = Instantiate((UseButton)buttonPrefab, _buttonsHolder);
    //    button.Init(_pair);

    //    if (HasTimer())
    //    {
    //        _pair.Time = _timer.GetExpirationTimeString(_config);

    //        _shopCanvas.SaveData();
    //        SetTimer();
    //    }
    //}

    //public void SwapToPurchaseButton(BaseShopButton buttonPrefab)
    //{
    //    int childs = _buttonsHolder.childCount;

    //    for (int i = childs - 1; i > -1; i--)
    //    {
    //        Destroy(_buttonsHolder.GetChild(i).gameObject);
    //    }

    //    foreach (var item in _config.PurchaseButtonPricePairs)
    //    {
    //        var button = Instantiate((PurchaseButton)buttonPrefab, _buttonsHolder);
    //        button.Init(this, item, _purchaseManager, _pair);
    //    }

    //    if (HasTimer())
    //    {
    //        _pair.Time = _timer.GetExpirationTimeString(_config);

    //        _shopCanvas.SaveData();
    //        SetTimer();
    //    }
    //}

    public void SwapToButton(BaseShopButton buttonPrefab) 
    {
        int childs = _buttonsHolder.childCount;

        for (int i = childs - 1; i > -1; i--)
        {
            Destroy(_buttonsHolder.GetChild(i).gameObject);
        }

        if (buttonPrefab is UseButton) 
        {
            var button = Instantiate((UseButton)buttonPrefab, _buttonsHolder);
            button.Init(_pair);

            if (HasTimer())
            {
                _pair.Time = _timer.GetExpirationTimeString(_config);

                _shopCanvas.SaveData();
                SetTimer();
            }
            return;
        }

        foreach (var item in _config.PurchaseButtonPricePairs)
        {
            var button = Instantiate((PurchaseButton)buttonPrefab, _buttonsHolder);
            button.Init(this, item, _purchaseManager, _pair);
        }
    }


    private void OnTimeOut() 
    {
        _timerHolder.SetActive(false);
        _shopCanvas.UpdateItemOnTimeOut(this);
    }

    private void OnDestroy()
    {
        _timer.TimeOut -= OnTimeOut;
    }
}
