using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GoogleAdsRewardAd : MonoBehaviour
{
    private RewardedAd rewardedAd;

    private void Start()
    {
        Debug.LogError("Rewarded Google Ad is initialized...");

        this.rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        //this.rewardedAd = new RewardedAd("ca-app-pub-6113693617274213/4772176572");

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        if (CurrentLevelData.isRewardedAvaiable)
        {
            var points = FilesJobs.SaveGameStateExtraReward();
            CurrentLevelData.isRewardedAvaiable = false;

            if (points == 0)
            {
                GameObject.Find("BonusInfo").GetComponentInChildren<Text>().text = "";
            }
            else
            {
                GameObject.Find("BonusInfo").GetComponentInChildren<Text>().text = "BONUS!  +" + points;
            }
        }
    }

    public void ShowRewardAd()
    {
        Debug.Log("Show reward run!");
        this.rewardedAd.Show();
    }
}
