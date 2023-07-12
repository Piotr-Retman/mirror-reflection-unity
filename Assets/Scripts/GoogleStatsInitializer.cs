using UnityEngine;
using UnityEngine.UI;

class GoogleStatsInitializer : MonoBehaviour
{
    private void Start()
    {
        Button button = GameObject.Find("GoToGoogleStats")
        .GetComponent<Button>();
        button.interactable = CurrentUserOptions.googlePlayGames;
    }
}
