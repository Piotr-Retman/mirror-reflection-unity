using System;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

class GooglePlayGamesFeatures : MonoBehaviour
{

    private Text authStatus;
    private Text signInButtonText;

    private void Start()
    {
        signInButtonText = GameObject.Find("SignInBtn").GetComponentInChildren<Text>();
        authStatus = GameObject.Find("SignInStatus").GetComponentInChildren<Text>();
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Change sign-in button text
            signInButtonText.text = "Sign out";
        }
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            signInButtonText.text = CurrentLanguageData.LANGUAGE_DICTIONARY["SIGN_IN"];
            authStatus.text = "Signed Out!";
        }
        Debug.Log("signInButton clicked!");
    }

    private void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(Mirror Reflection) Signed in!");

            // Change sign-in button text
            signInButtonText.text = CurrentLanguageData.LANGUAGE_DICTIONARY["SIGN_OUT"];

            // Show the user's name
            authStatus.text = CurrentLanguageData.LANGUAGE_DICTIONARY["SIGN_TXT"] + Social.localUser.userName;
        }
        else
        {
            Debug.Log("(Mirror Reflection) Sign-in failed...");

            // Show failure message
            signInButtonText.text = CurrentLanguageData.LANGUAGE_DICTIONARY["SIGN_IN"];
            authStatus.text = "Sign-in failed!";
        }
    }

    public void ShowLeaderBoards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    private void loadCallback(IScore[] obj)
    {
        Debug.Log(obj[0]);
    }

    public void ShowAchivementsBoards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.Log("Cannot show achivements: not authenticated");
        }
    }
}
