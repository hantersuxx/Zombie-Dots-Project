using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    BannerView BannerView { get; set; }

    private void Start()
    {
        GameManager.Instance.Storage.CurrentVersion.text = $"BUILD {Application.version}";
        MobileAds.Initialize(Globals.AdmobAppId);
        RequestBanner();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Extensions.Log(GetType(), "Quit pressed");
            Application.Quit();
        }
    }

    private void RequestBanner()
    {
        BannerView = new BannerView(Globals.ZombieDotsBanner, AdSize.SmartBanner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        BannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        BannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        BannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        BannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        BannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;


        AdRequest request = new AdRequest.Builder()
            .AddExtra("max_ad_content_rating", "PG")
            //.AddExtra("tag_for_under_age_of_consent", "true")
            .Build();
        BannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }

    public void Play()
    {
        GameManager.Instance.Storage.SceneFader.FadeTo(GameManager.Instance.Storage.LevelSelect);
    }

    public void Achievements()
    {
        Extensions.Log(GetType(), "Achievements pressed");
        GameManager.Instance.GooglePlayServices.OnAchievementsClick();
    }

    public void Leaderboard()
    {
        Extensions.Log(GetType(), "Leaderboard pressed");
        GameManager.Instance.GooglePlayServices.OnLeaderboardClick();
    }
}
