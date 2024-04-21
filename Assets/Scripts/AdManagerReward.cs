using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

public class AdManagerReward : MonoBehaviour
{
    [SerializeField] UnityEvent OnFailedToLoad;
    [SerializeField] UnityEvent OnRewarded;
    // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-7198482875251564/3519581567";
    #elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
    #else
    private string _adUnitId = "unused";
    #endif

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private RewardedAd rewardedAd;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
                rewardedAd.Destroy();
                rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    OnFailedToLoad.Invoke();
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                            + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
                this.OnRewarded.Invoke();
            });
        }
    }
}
