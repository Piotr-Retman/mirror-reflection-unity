using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void GoToMainScene() => SceneManager.LoadScene(sceneName: "AssetProject");

    public void GoToOptionsScene() => SceneManager.LoadScene(sceneName: "Options");

    public void GoToStats()
    {
        UpdateCurrentLevelData();
        SceneManager.LoadScene(sceneName: "Stats");
    }

    public void GoToSumupStats()
    {
        PrepareSumUpData();
        FilesJobs.SaveGameState();
        SceneManager.LoadScene(sceneName: "LevelSumup");
    }

    private void PrepareSumUpData()
    {
        SumUpLevelData.levelNumber = CurrentLevelData.actualLevel.ToString();
        SumUpLevelData.pathNumber = CurrentLevelData.path.ToString();
        SumUpLevelData.allPoints = (CurrentLevelData.currentPoints + CurrentLevelData.loadedPoints).ToString();
    }

    public void GoToChooseLevelScene()
    {
        UpdateCurrentLevelData();
        SceneManager.LoadScene(sceneName: "ChooseLvlScene");
    }

    public void GoToGoogleGamesStats()
    {
        UpdateCurrentLevelData();
        SceneManager.LoadScene(sceneName: "GooglePlayServices");
    }


    private void UpdateCurrentLevelData()
    {
        FilesJobs fj = new FilesJobs();
        fj.LoadGame();
    }
}
