﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdsCtrl2 : MonoBehaviour {

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;

    public static string OutputMessage
    {
        set { outputMessage = value; }
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

        //MobileAds.SetiOSAppPauseOnBackground(true);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        //// Get singleton reward based video ad reference.
        //this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        //// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        //this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        //this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        //this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        //this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        //this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        //this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        //this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

        RequestBanner();
        RequestInterstitial();
    }

    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
    }

    /*public void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        style.alignment = TextAnchor.LowerRight;
        style.fontSize = (int)(Screen.height * 0.06);
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float fps = 1.0f / this.deltaTime;
        string text = string.Format("{0:0.} fps", fps);
        GUI.Label(rect, text, style);

        // Puts some basic buttons onto the screen.
        GUI.skin.button.fontSize = (int)(0.035f * Screen.width);
        float buttonWidth = 0.35f * Screen.width;
        float buttonHeight = 0.15f * Screen.height;
        float columnOnePosition = 0.1f * Screen.width;
        float columnTwoPosition = 0.55f * Screen.width;

        Rect requestBannerRect = new Rect(
            columnOnePosition,
            0.05f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(requestBannerRect, "Request\nBanner"))
        {
            this.RequestBanner();
        }

        Rect destroyBannerRect = new Rect(
            columnOnePosition,
            0.225f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(destroyBannerRect, "Destroy\nBanner"))
        {
            this.bannerView.Destroy();
        }

        Rect requestInterstitialRect = new Rect(
            columnOnePosition,
            0.4f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(requestInterstitialRect, "Request\nInterstitial"))
        {
            this.RequestInterstitial();
        }

        Rect showInterstitialRect = new Rect(
            columnOnePosition,
            0.575f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(showInterstitialRect, "Show\nInterstitial"))
        {
            this.ShowInterstitial();
        }

        Rect destroyInterstitialRect = new Rect(
            columnOnePosition,
            0.75f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(destroyInterstitialRect, "Destroy\nInterstitial"))
        {
            this.interstitial.Destroy();
        }

        Rect requestRewardedRect = new Rect(
            columnTwoPosition,
            0.05f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(requestRewardedRect, "Request\nRewarded Video"))
        {
            this.RequestRewardBasedVideo();
        }

        Rect showRewardedRect = new Rect(
            columnTwoPosition,
            0.225f * Screen.height,
            buttonWidth,
            buttonHeight);
        if (GUI.Button(showRewardedRect, "Show\nRewarded Video"))
        {
            this.ShowRewardBasedVideo();
        }

        Rect textOutputRect = new Rect(
            columnTwoPosition,
            0.925f * Screen.height,
            buttonWidth,
            0.05f * Screen.height);
        GUI.Label(textOutputRect, outputMessage);
    }*/

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

        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        RegisterDelegateForInterstitial();

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
    }

    private void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            MonoBehaviour.print("Interstitial is not ready yet");
        }
    }

    private void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
        else
        {
            MonoBehaviour.print("Reward based video ad is not ready yet");
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

    public void ShowsBanner()
    {
        bannerView.Show();
    }

    public void ShowsInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        ShowsBanner();
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
