using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class GoogleMobileAdsScript : MonoBehaviour
{
    private BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {
        string appId = "ca-app-pub-6113693617274213~5083295709";
        // Initialize the Google Mobile Ads SDK.

        MobileAds.Initialize(appId);
        this.RequestBanner();

    }

    private void RequestBanner()
    {
       string bannerId = "ca-app-pub-6113693617274213/9294288867";

       // string fake = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the bottom of the screen.
        this.bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.BottomRight);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;

        this.bannerView.Show();

    }

    private void OnDestroy()
    {
        if (this.bannerView != null)
        {
            print("DESTROY BANNER");
            this.bannerView.Destroy();
        }
    }

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

}
