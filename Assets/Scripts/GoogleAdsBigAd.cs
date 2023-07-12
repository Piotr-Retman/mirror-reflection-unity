using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoogleAdsBigAd : MonoBehaviour
{
    private InterstitialAd interstitial;

    private ArrayList levelNumbersWhichWillActivate = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelNumbersWhichWillActivate();
        string appId = "ca-app-pub-6113693617274213~5083295709";
        MobileAds.Initialize(appId);
        this.RequestInterstitialAd();

    }

    private void UpdateLevelNumbersWhichWillActivate()
    {
        levelNumbersWhichWillActivate.Add(4);
        levelNumbersWhichWillActivate.Add(12);
        levelNumbersWhichWillActivate.Add(14);
    }

    private void RequestInterstitialAd()
    {
        //string fake = "ca-app-pub-3940256099942544/1033173712";
        string real = "ca-app-pub-6113693617274213/9515473037";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(real);
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        OnDestroy();
        this.interstitial.Destroy();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("ChooseLvlScene");
    }

    public void ShowAd()
    {
        Debug.Log("Ad showing...");
        if (this.interstitial != null && this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
}
