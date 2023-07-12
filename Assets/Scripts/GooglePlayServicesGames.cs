using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.EventSystems;
using GooglePlayGames;
using System;
using UnityEngine.UI;

public class GooglePlayServicesGames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

        if (CurrentUserOptions.googlePlayGames)
        {
            //  ADD THIS CODE BETWEEN THESE COMMENTS
            TurnOnButtons();
            // Create client configuration
            PlayGamesClientConfiguration config = new
                PlayGamesClientConfiguration.Builder()
                .Build();

            // Enable debugging output (recommended)
            PlayGamesPlatform.DebugLogEnabled = true;

            // Initialize and activate the platform
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            // END THE CODE TO PASTE INTO START
            PlayGamesPlatform.Instance.Authenticate(SignInWhenTurnOnCallback, true);
        }
    }

    private void TurnOnButtons()
    {
        var rewardTrophyGO = GameObject.Find("ShowLeaders");
        var achivementsGO = GameObject.Find("ShowAchivements");
        if(rewardTrophyGO != null && achivementsGO != null)
        {
            Button rewardTrophy = rewardTrophyGO.GetComponent<Button>();
            rewardTrophy.interactable = true;

            Button achivements = achivementsGO.GetComponent<Button>();
            achivements.interactable = true;
        }
    }

    private void SignInWhenTurnOnCallback(bool obj)
    {
        Debug.Log("Signed in!");
    }
}
