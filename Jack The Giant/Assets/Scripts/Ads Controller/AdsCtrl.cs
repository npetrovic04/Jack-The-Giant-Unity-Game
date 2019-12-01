using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdsCtrl : MonoBehaviour
{
    public static AdsCtrl instance;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    void Awake()
    {
        MakeSingleton();
    }

    public void Start()
    {

#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544~3347511713";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);


        RequestBanner();
        RequestInterstitial();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    //void OnLevelWasLoaded()
    //{
    //    if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "OptionsMenu" || SceneManager.GetActiveScene().name == "HighscoreScene")
    //    {
    //        if (UnityEngine.Random.Range(0, 10) > 4)
    //        {
    //            ShowsBanner();
    //            ShowInterstitial();
    //        }
    //    }
    //}

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" || scene.name == "OptionsMenu" || scene.name == "HighscoreScene")
        {
            if (UnityEngine.Random.Range(0, 10) > 7)
            {
              //  ShowsBanner();
                ShowInterstitial();
            }
        }
    }

    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
    }

    

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

        RegisterDelegateForBanner();

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }

    private void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        //// Clean up interstitial ad before creating a new one.
        //if (this.interstitial != null)
        //{
        //    this.interstitial.Destroy();
        //}

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        RegisterDelegateForInterstitial();

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }

    
    void RegisterDelegateForBanner()
    {
        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;
    }

    void UnregisterDelegateForBanner()
    {
        // Register for ad events.
        this.bannerView.OnAdLoaded -= this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad -= this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening -= this.HandleAdOpened;
        this.bannerView.OnAdClosed -= this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication -= this.HandleAdLeftApplication;
    }

    void RegisterDelegateForInterstitial()
    {
        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
    }

    void UnregisterDelegateForInterstitial()
    {
        // Register for ad events.
        this.interstitial.OnAdLoaded -= this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad -= this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening -= this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed -= this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication -= this.HandleInterstitialLeftApplication;
    }

    //public void ShowsBanner()
    //{
    //    bannerView.Show();
    //}

    //public void ShowsInterstitial()
    //{
    //    if (interstitial.IsLoaded())
    //    {
    //        interstitial.Show();
    //    }
    //    else
    //    {
    //        RequestInterstitial();
    //    }
    //}

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
      //  ShowsBanner();
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UnregisterDelegateForBanner();
        RequestBanner();
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        UnregisterDelegateForBanner();
        RequestBanner();
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UnregisterDelegateForInterstitial();
        RequestInterstitial();
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        UnregisterDelegateForInterstitial();
        RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}
