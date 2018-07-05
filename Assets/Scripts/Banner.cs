using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
    private BannerView BannerView { get; set; }

    private void Start()
    {
        MobileAds.Initialize(Globals.AdmobAppId);
        RequestBanner();
    }

    private void OnDestroy()
    {
        BannerView.Destroy();
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
            .AddExtra(Globals.MaxAdContentRating.key, Globals.MaxAdContentRating.rating)
            //.AddExtra("tag_for_under_age_of_consent", "true")
            .Build();
        BannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Extensions.Log(GetType(), $"HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Extensions.Log(GetType(), $"HandleFailedToReceiveAd event received with message: {args.Message}");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Extensions.Log(GetType(), $"HandleAdClosed event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Extensions.Log(GetType(), $"HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Extensions.Log(GetType(), $"HandleAdLeavingApplication event received");
    }
}
