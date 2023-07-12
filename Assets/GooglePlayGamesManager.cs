using GooglePlayGames;
using UnityEngine;

public class GooglePlayGamesManager : MonoBehaviour
{
    public static void SaveGeneralScore(long score)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_mirror_reflection__general_leaderboard, (bool success) =>
            {
                Debug.Log("(Mirror Reflection) Leaderboard update success: " + success);
            });
        }
    }


}
