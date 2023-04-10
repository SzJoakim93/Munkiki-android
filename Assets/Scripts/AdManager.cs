using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AdManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnAdClosedEvent;
    private InterstitialAd interstitial;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        
    }

    public static void Initialize()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestInterstitial()
    {
        #if UNITY_ANDROID
            //string adUnitId = "ca-app-pub-7198482875251564/5225174504";
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #elif UNITY_EDITOR
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";    
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when the ad is closed.
        this.interstitial.OnAdClosed += (sender, args) => this.OnAdClosedEvent.Invoke();
        this.interstitial.OnAdClosed += CleanUpInterstial;
        Debug.Log("Ad loaded");
    }

    public bool ShowInterstitial()
    {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
            Debug.Log("Ad shown");
            return true;
        }

        Debug.Log ("Ad Not ready");
        return false;
    }

    public void CleanUpInterstial(object sender, EventArgs args)
    {
        interstitial.Destroy();
    }


}
