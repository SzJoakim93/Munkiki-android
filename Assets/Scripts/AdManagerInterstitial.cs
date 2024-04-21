using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

public class AdManagerInterstitial : MonoBehaviour
{
    [SerializeField] UnityEvent OnAdClosedEvent;
    [SerializeField] UnityEvent OnAdOpeningEvent;
    private InterstitialAd interstitialAd;

    // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-7198482875251564/7542966918";
    #elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
    #else
    private string _adUnitId = "unused";
    #endif

    private bool isEnabled;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        isEnabled = PlayerPrefs.GetInt("IsAdsRemoved", 0) == 0;
        if (isEnabled)
        {
            LoadInterstitialAd();
            RegisterEventHandlers();
        }
    }

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        if (!isEnabled)
        {
            return;
        }

        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
                interstitialAd.Destroy();
                interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                            + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public bool ShowAd()
    {
        Debug.Log(Time.timeSinceLevelLoad);
        if (!isEnabled
            || Time.timeSinceLevelLoad < 30.0f
            || Time.timeSinceLevelLoad < 180.0f && Global.isInterstitialAdPalyedPrev)
        {
            Global.isInterstitialAdPalyedPrev = false;
            return false;
        }

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            Global.isInterstitialAdPalyedPrev = true;
            interstitialAd.Show();
            return true;
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            return false;
        }
    }

    private void OnDestroy()
    {
        interstitialAd.Destroy();
    }

    private void RegisterReloadHandler(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                        "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }

    private void RegisterEventHandlers()
    {
        // Called when an ad is shown.
        this.interstitialAd.OnAdFullScreenContentOpened += () => this.OnAdOpeningEvent.Invoke();
        // Called when the ad is closed.
        this.interstitialAd.OnAdFullScreenContentClosed += () => this.OnAdClosedEvent.Invoke();
    }
}
