using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdsControll : MonoBehaviour
{

    public static AdsControll instance = null;
    public string Android_Admob_Banner_ID;          //ca-app-pub-3940256099942544/6300978111 -> ovo je Test Ad
    public string Android_Admob_Interstitial_ID;    //ca-app-pub-3940256099942544/1033173712

    //public bool testMode;
    BannerView bannerView;              // container za Banner reklamu
    InterstitialAd interstitial;
    AdRequest request;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RequestBanner()
    {
        bannerView = new BannerView(Android_Admob_Banner_ID, AdSize.SmartBanner, AdPosition.Top);

        // Create an empty ad request
        AdRequest adRequest = new AdRequest.Builder().Build();

        // Load banner with request
        bannerView.LoadAd(adRequest);
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void HideBanner(float duration)
    {
        StartCoroutine("HideBannerRoutine", duration);
    }

    IEnumerator HideBannerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        bannerView.Hide();
    }

    void RequestInterstitial()
    {
        interstitial = new InterstitialAd(Android_Admob_Interstitial_ID);

        // Create an empty ad request
        request = new AdRequest.Builder().Build();

        // Load banner with request
        interstitial.LoadAd(request);

        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += LevelFinishedLoading;

        RequestBanner();

        RequestInterstitial();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;

        bannerView.Destroy();

        interstitial.Destroy();
    }
}
