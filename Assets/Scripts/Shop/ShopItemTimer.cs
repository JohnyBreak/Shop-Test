using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ShopItemTimer : MonoBehaviour
{
    private const string _url = "http://worldtimeapi.org/api/ip";

    public Action TimeOut;

    private DateTime _expirationTime;
    private string _nowTime;
    private string _format = @"hh\:mm\:ss";

    public string GetExpirationTimeString(ShopItemConfig config) 
    {
        DateTime now = DateTime.Now;

        _expirationTime = new DateTime(now.Year, now.Month, now.Day,
            now.Hour + config.Hour,
            now.Minute + config.Min,
            now.Second + config.Sec);

        return _expirationTime.ToString("u", CultureInfo.InvariantCulture);
    }

    public void StartCountDown(TextMeshProUGUI timerText, string savedTime) 
    {
        _expirationTime = DateTime.ParseExact(savedTime, "u", CultureInfo.InvariantCulture);

        StartCoroutine(CountDownRoutine(timerText));
    }

    private IEnumerator CountDownRoutine(TextMeshProUGUI timerText) 
    {
        timerText.text = GetSpanString();

        while (DateTime.Now < _expirationTime) 
        {
            yield return new WaitForSeconds(1f); 

            timerText.text = GetSpanString();
        }
        TimeOut?.Invoke();
    }

    private string GetSpanString() 
    {
        TimeSpan span = _expirationTime - DateTime.Now;
        return span.ToString(_format);
    }

    public bool NeedToSetTimer(string savedTime)
    {
        DateTime savedExpirationTime = DateTime.ParseExact(savedTime, "u", CultureInfo.InvariantCulture);

        if (savedExpirationTime > DateTime.Now) return true;

        return false;
    }

}
