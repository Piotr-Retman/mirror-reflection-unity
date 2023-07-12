using UnityEngine;
using UnityEngine.UI;

public class OptionsWorker : MonoBehaviour
{

    public void HandleMusicOn()
    {
        var musicOnToggle = GameObject.Find("MusicOnToggle");
        var isOn = musicOnToggle.GetComponent<Toggle>().isOn;
        CurrentUserOptions.playMusic = isOn;
    }

    public void HandleInifiniteChancesOn()
    {
        var infiniteChances = GameObject.Find("InfiniteChances");
        var isOn = infiniteChances.GetComponent<Toggle>().isOn;
        CurrentUserOptions.infiniteChances = isOn;
    }

    public void HandleSoundOn()
    {
        var soundOn = GameObject.Find("SoundOn");
        var isOn = soundOn.GetComponent<Toggle>().isOn;
        CurrentUserOptions.soundsOn = isOn;
    }

    public void HandleCountTime()
    {
        var timingToggle = GameObject.Find("TimingToggle");
        var isOn = timingToggle.GetComponent<Toggle>().isOn;
        CurrentUserOptions.countTime = isOn;
    }

    public void HandleGooglePlayServicesOn()
    {
        var googlePlayGamesOnTgl = GameObject.Find("GooglePlayGamesOn");
        var isOn = googlePlayGamesOnTgl.GetComponent<Toggle>().isOn;
        CurrentUserOptions.googlePlayGames = isOn;
    }

    public void SaveOptions()
    {
        var filesObj = new FilesJobs();
        filesObj.SaveOptions();
        PointsCoefficient.UpdateCoefficient();
        Debug.Log("Points Coefficient: " + PointsCoefficient.COEFFICIENT);
    }
}
