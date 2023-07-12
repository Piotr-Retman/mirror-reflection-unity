using UnityEngine;
using UnityEngine.UI;

public class OptionsBehavior : MonoBehaviour
{
    void Start()
    {
        CheckMusicOn();
        CheckInfiniteChances();
        CheckCountTime();
        CheckSoundOn();
        CheckGooglePlayGamesServicesOn();
    }

    // Visualisation of Options
    public void CheckMusicOn()
    {
        var musicOnToggle = GameObject.Find("MusicOnToggle");
        musicOnToggle.GetComponent<Toggle>().isOn = CurrentUserOptions.playMusic;
    }

    public void CheckInfiniteChances()
    {
        var infiniteChancesToggle = GameObject.Find("InfiniteChances");
        infiniteChancesToggle.GetComponent<Toggle>().isOn = CurrentUserOptions.infiniteChances;
    }

    public void CheckCountTime()
    {
        var checkTimeToggle = GameObject.Find("TimingToggle");
        checkTimeToggle.GetComponent<Toggle>().isOn = CurrentUserOptions.countTime;
    }

    public void CheckSoundOn()
    {
        var checkSoundOn = GameObject.Find("SoundOn");
        checkSoundOn.GetComponent<Toggle>().isOn = CurrentUserOptions.soundsOn;
    }

    public void CheckGooglePlayGamesServicesOn()
    {
        var checkSoundOn = GameObject.Find("GooglePlayGamesOn");
        checkSoundOn.GetComponent<Toggle>().isOn = CurrentUserOptions.googlePlayGames;
    }
}
