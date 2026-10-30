using UnityEngine;
using Unity.Services.LevelPlay;

public class AdService : MonoBehaviour
{
    public static AdService Instance { get; private set; }

    [Header("Android IDs")]
    public string androidAppKey = "ANDROID_APP_KEY";
    public string androidBannerId = "ANDROID_BANNER_AD_UNIT";
    public string androidRewardedId = "ANDROID_REWARDED_AD_UNIT";

    private LevelPlayBannerAd _bannerAd;
    private LevelPlayRewardedAd _rewardedAd;

    private string AppKey => GetPlatformKey();
    private string BannerId => GetBannerId();
    private string RewardedId => GetRewardedId();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeLevelPlay();
    }

    void InitializeLevelPlay()
    {
        LevelPlay.OnInitSuccess += HandleInitSuccess;
        LevelPlay.OnInitFailed += HandleInitFailed;

        LevelPlay.Init(AppKey);
    }

    private void HandleInitSuccess(LevelPlayConfiguration config)
    {
        Debug.Log("LevelPlay init success");
        CreateBanner();
        CreateRewarded();
    }

    private void HandleInitFailed(LevelPlayInitError error)
    {
        Debug.LogError($"LevelPlay init failed: {error.ErrorMessage}");
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            LevelPlay.OnInitSuccess -= HandleInitSuccess;
            LevelPlay.OnInitFailed -= HandleInitFailed;
        }
    }

    void CreateBanner()
    {
        _bannerAd = new LevelPlayBannerAd(BannerId);
        _bannerAd.OnAdLoaded += info => Debug.Log("Banner loaded");
        _bannerAd.OnAdLoadFailed += error => Debug.LogError($"Banner load failed: {error}");
        _bannerAd.OnAdDisplayed += info => Debug.Log("Banner displayed");
        _bannerAd.OnAdDisplayFailed += (info, error) => Debug.LogError($"Banner display failed: {error}");
    }

    void CreateRewarded()
    {
        _rewardedAd = new LevelPlayRewardedAd(RewardedId);
        _rewardedAd.OnAdLoaded += info => Debug.Log("Rewarded loaded");
        _rewardedAd.OnAdLoadFailed += error => Debug.LogError($"Rewarded load failed: {error}");
        _rewardedAd.OnAdRewarded += (info, reward) => {
            Debug.Log($"Rewarded granted: {reward.Name} x{reward.Amount}");
        };
        _rewardedAd.OnAdClosed += info => Debug.Log("Rewarded closed");
        _rewardedAd.OnAdDisplayFailed += (info, error) => Debug.LogError($"Rewarded display failed: {error}");
    }

    public void LoadBanner() => _bannerAd?.LoadAd();

    public void ShowBanner()
    {
        _bannerAd?.ShowAd();
    }

    public void HideBanner() => _bannerAd?.HideAd();

    public void LoadRewarded() => _rewardedAd?.LoadAd();

    public void ShowRewarded()
    {
        if (_rewardedAd != null && _rewardedAd.IsAdReady())
            _rewardedAd.ShowAd();
        else
            Debug.LogWarning("Rewarded not ready");
    }

    string GetPlatformKey()
    {
#if UNITY_ANDROID
        return androidAppKey;
#elif UNITY_IOS
        return iosAppKey;
#else
        return "UNSUPPORTED_PLATFORM";
#endif
    }

    string GetBannerId()
    {
#if UNITY_ANDROID
        return androidBannerId;
#elif UNITY_IOS
        return iosBannerId;
#else
        return "UNSUPPORTED_BANNER";
#endif
    }

    string GetRewardedId()
    {
#if UNITY_ANDROID
        return androidRewardedId;
#elif UNITY_IOS
        return iosRewardedId;
#else
        return "UNSUPPORTED_REWARDED";
#endif
    }
}